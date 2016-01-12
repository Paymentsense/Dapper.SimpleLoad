using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Impl
{
    public class QueryBuilder
    {
        private int _linkTableAliasIndex;
        //  TODO: You are DEFINITELY going to want to cache these bad boys!

        //  TODO: split on columns - make sure these are actually the PK columns otherwise it'll all go tits up (really they need to be unique to the object concerned or, due to Dapper limitations, it'll go wrong, but there's nothing you can do about that in SimpleLoad)

        public IQuery BuildQuery(
            TypePropertyMap map,
            string [] aliases,
            string whereClauseExpression,
            object parameters,
            int desiredNumberOfResults)
        {
            var query = new Query();
            var selectListBuff = new StringBuilder();
            var countBuff = new StringBuilder();
            var fromAndJoinsBuff = new StringBuilder();
            var whereConditionBuff = new StringBuilder();
            var splitOn = new StringBuilder();

            if (desiredNumberOfResults > 0)
            {
                //  I wouldn't normally do this with a non-parameterised value but you can't
                //  do SQL injection just using a positive integer.
                countBuff.Append("TOP (");
                countBuff.Append(desiredNumberOfResults);
                countBuff.Append(") ");
            }

            for (int index = 0, size = map.Count; index < size; ++index)
            {
                var entry = map[index];
                var metadata = entry.Metadata;
                var alias = string.IsNullOrEmpty(whereClauseExpression) ? entry.Alias : aliases[index];
                var firstColumn = SelectListBuilder.AppendSelectListAndGetFirstColumnFor(
                    selectListBuff,
                    metadata,
                    index > 0,
                    alias);

                if (index > 0)
                {
                    if (splitOn.Length > 0)
                    {
                        splitOn.Append(", ");
                    }
                    splitOn.Append(firstColumn);
                }

                if (index == 0)
                {
                    var table = metadata.GetAttribute<TableAttribute>();

                    fromAndJoinsBuff.Append("FROM ");
                    AppendTableNameAndAlias(fromAndJoinsBuff, table, alias);

                    if (string.IsNullOrEmpty(whereClauseExpression))
                    {
                        BuildWhereCondition(parameters, whereConditionBuff, entry, aliases);
                    }
                    else
                    {
                        whereConditionBuff.Append("WHERE ");
                        whereConditionBuff.Append(whereClauseExpression);
                    }
                }
                else
                {
                    AppendJoin(map, index, entry, alias, fromAndJoinsBuff, metadata, aliases);
                }
            }

            query.Sql = string.Format(@"SELECT {0} {1}
                {2}{3};", countBuff, selectListBuff, fromAndJoinsBuff, whereConditionBuff);
            query.SplitOn = splitOn.ToString();
            return query;
        }

        private void AppendJoin(
            TypePropertyMap map,
            int index,
            TypePropertyMapEntry entry,
            string aliasForCurrentTable,
            StringBuilder fromAndJoinsBuff,
            DtoMetadata metadata,
            string [] aliases)
        {
            var target = map.GetEntryWithMatchingPropertyPreceding(index, entry.Type);
            if (target == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unable to find any property "
                        + "that fits type '{0}'. Please ensure you have included all the necessary "
                        + "types in your query, and that any type to which '{0}' should be added "
                        + "precedes it in the list of types otherwise, like soldiers killed in "
                        + "Stanley Kubrick's Full Metal Jacket, you will be in a world of s***.",
                        entry.Type));
            }

            fromAndJoinsBuff.Append("LEFT OUTER JOIN ");

            var targetProperty = target.GetPropertyMetadataFor(entry.Type);

            if (targetProperty.HasAttribute<ManyToManyAttribute>())
            {
                var manyToMany = targetProperty.GetAttribute<ManyToManyAttribute>();
                var linkAlias = AppendTableNameAndAlias(fromAndJoinsBuff, manyToMany.SchemaQualifiedLinkTableName);

                fromAndJoinsBuff.Append("    ON ");
                AppendJoinConditionArgument(target, fromAndJoinsBuff, target.Metadata.PrimaryKey, aliases);
                fromAndJoinsBuff.Append(" = ");
                AppendJoinConditionArgument(fromAndJoinsBuff, target.Metadata.PrimaryKey, linkAlias);

                fromAndJoinsBuff.Append(Environment.NewLine);

                fromAndJoinsBuff.Append("LEFT OUTER JOIN ");

                var table = metadata.GetAttribute<TableAttribute>();
                AppendTableNameAndAlias(fromAndJoinsBuff, table, aliasForCurrentTable);

                fromAndJoinsBuff.Append("    ON ");
                AppendJoinConditionArgument(entry, fromAndJoinsBuff, metadata.PrimaryKey, aliases);
                fromAndJoinsBuff.Append(" = ");
                AppendJoinConditionArgument(fromAndJoinsBuff, metadata.PrimaryKey, linkAlias);

                fromAndJoinsBuff.Append(Environment.NewLine);
            }
            else
            {
                var table = metadata.GetAttribute<TableAttribute>();

                AppendTableNameAndAlias(fromAndJoinsBuff, table, aliasForCurrentTable);

                fromAndJoinsBuff.Append("    ON ");
                if (targetProperty.HasAttribute<OneToOneAttribute>() && string.IsNullOrEmpty(targetProperty.GetAttribute<OneToOneAttribute>().ChildForeignKeyColumn))
                {
                    //  Covers situation where foreign key column is on the target table
                    AppendJoinConditionArgument(entry, fromAndJoinsBuff, metadata.PrimaryKey, aliases);
                    fromAndJoinsBuff.Append(" = ");
                    AppendJoinConditionArgument(target, fromAndJoinsBuff, targetProperty, aliases);
                }
                else if (targetProperty.HasAttribute<ManyToOneAttribute>())
                {
                    var manyToOne = targetProperty.GetAttribute<ManyToOneAttribute>();
                    var targetColumn = manyToOne.ForeignKeyTargetColumnName;
                    if (string.IsNullOrEmpty(targetColumn))
                    {
                        AppendJoinConditionArgument(entry, fromAndJoinsBuff, metadata.PrimaryKey, aliases);
                    }
                    else
                    {
                        AppendJoinConditionArgument(entry, fromAndJoinsBuff, targetColumn, aliases);
                    }

                    fromAndJoinsBuff.Append(" = ");
                    AppendJoinConditionArgument(target, fromAndJoinsBuff, targetProperty, aliases);
                }
                else if (targetProperty.HasAttribute<OneToOneAttribute>() || targetProperty.HasAttribute<OneToManyAttribute>())
                {
                    //  Covers situation where foreign key column is on the source table
                    AppendJoinConditionArgument(entry, fromAndJoinsBuff, entry.GetPropertyMetadataFor(target.Type), aliases);
                    fromAndJoinsBuff.Append(" = ");
                    AppendJoinConditionArgument(target, fromAndJoinsBuff, target.Metadata.PrimaryKey, aliases);
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Unable to generate JOIN condition between types '{0}' and '{1}' because the property '{2}' on '{1}' "
                            + "is not decorated with an attribute indicating its cardinality. Please add a [OneToOne], [OneToMany] "
                            + "[ManyToOne], or [ManyToMany] decoration, as appropriate.",
                            metadata.DtoType,
                            target.Type,
                            targetProperty.Prop.Name));
                }

                fromAndJoinsBuff.Append(Environment.NewLine);
            }
        }

        private void AppendTableNameAndAlias(StringBuilder fromAndJoinsBuff, TableAttribute table, string alias)
        {
            var schemaQualifiedTableName = table.SchemaQualifiedTableName;
            AppendTableNameAndAlias(fromAndJoinsBuff, schemaQualifiedTableName, alias);
        }

        private string AppendTableNameAndAlias(StringBuilder fromAndJoinsBuff, string schemaQualifiedTableName)
        {
            var linkAlias = AliasGenerator.GenerateAliasFor(schemaQualifiedTableName, _linkTableAliasIndex++);
            AppendTableNameAndAlias(
                fromAndJoinsBuff,
                schemaQualifiedTableName,
                linkAlias);
            return linkAlias;
        }

        private static void AppendTableNameAndAlias(StringBuilder fromAndJoinsBuff, string schemaQualifiedTableName, string alias)
        {
            fromAndJoinsBuff.Append(schemaQualifiedTableName);
            fromAndJoinsBuff.Append(" AS ");
            fromAndJoinsBuff.Append(alias);
            fromAndJoinsBuff.Append(Environment.NewLine);
        }

        private static void AppendJoinConditionArgument(
            TypePropertyMapEntry entry,
            StringBuilder fromAndJoinsBuff,
            PropertyMetadata property,
            string [] aliases)
        {
            if (property == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unable to generate JOIN condition because no suitable join property could "
                        + "be found on '{0}'. This might be because you haven't marked a property with "
                        + "the [PrimaryKey] attribute from Dapper.SimpleSave, or because no matching "
                        + "target property can be found on the target object, although this seems a bit "
                        + "unlikely. Also bear in mind that commparison with property names here is "
                        + "case-sensitive so this failure may be caused by a difference in casing between, "
                        + "say, the property name, and any column name specified in a [Column] attribute.",
                        entry.Type));
            }
            var alias = aliases == null ? entry.Alias : aliases[entry.Index];
            AppendJoinConditionArgument(fromAndJoinsBuff, property, alias);
        }

        private static void AppendJoinConditionArgument(
            TypePropertyMapEntry entry,
            StringBuilder fromAndJoinsBuff,
            string columnName,
            string [] aliases)
        {
            var alias = aliases == null ? entry.Alias : aliases[entry.Index];
            AppendJoinConditionArgument(fromAndJoinsBuff, columnName, alias);
        }

        private static void AppendJoinConditionArgument(StringBuilder fromAndJoinsBuff, PropertyMetadata property, string alias)
        {
            AppendJoinConditionArgument(fromAndJoinsBuff, property.ColumnName, alias);
        }

        private static void AppendJoinConditionArgument(StringBuilder fromAndJoinsBuff, string columnName, string alias)
        {
            fromAndJoinsBuff.Append(alias);
            fromAndJoinsBuff.Append(".[");
            fromAndJoinsBuff.Append(columnName);
            fromAndJoinsBuff.Append("]");
        }

        private static void BuildWhereCondition(object parameters, StringBuilder whereConditionBuff, TypePropertyMapEntry entry, string [] aliases)
        {
            if (parameters != null)
            {
                if (parameters is IEnumerable<KeyValuePair<string, object>>)
                {
                    foreach (var kvp in (IEnumerable<KeyValuePair<string, object>>) parameters)
                    {
                        AppendConditionForParameter(
                            whereConditionBuff,
                            entry,
                            kvp.Key,
                            kvp.Value,
                            aliases);
                    }
                }
                else
                {
                    foreach (
                        var property in
                            parameters.GetType()
                                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                    {
                        AppendConditionForParameter(
                            whereConditionBuff,
                            entry,
                            property.Name,
                            property.GetMethod.Invoke(parameters, new object[0]),
                            aliases);
                    }
                }
            }
        }

        private static void AppendConditionForParameter(
            StringBuilder whereConditionBuff,
            TypePropertyMapEntry entry,
            string parameterName,
            object parameterValue,
            string [] aliases)
        {
            if (whereConditionBuff.Length == 0)
            {
                whereConditionBuff.Append("WHERE ");
            }
            else
            {
                whereConditionBuff.Append("    AND ");
            }
            var alias = aliases == null ? entry.Alias : aliases[entry.Index];
            whereConditionBuff.Append(alias);
            whereConditionBuff.Append(".[");
            whereConditionBuff.Append(parameterName);
            whereConditionBuff.Append("] ");
            if (parameterValue == null)
            {
                whereConditionBuff.Append("IS NULL");
            }
            else
            {
                whereConditionBuff.Append("= @");
                whereConditionBuff.Append(parameterName);
            }
            whereConditionBuff.Append(Environment.NewLine);
        }
    }
}
