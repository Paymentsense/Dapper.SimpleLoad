using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Impl
{
    public static class QueryBuilder
    {
        //  TODO: You are DEFINITELY going to want to cache these bad boys!

        //  TODO: split on columns - make sure these are actually the PK columns otherwise it'll all go tits up (really they need to be unique to the object concerned or, due to Dapper limitations, it'll all go tits up, but there's nothing you can do about that in SimpleLoad)

        public static IQuery BuildQuery(
            TypePropertyMap map,
            string [] aliases,
            string whereClauseExpression,
            object parameters,
            int desiredNumberOfResults)
        {
            var query = new Query();
            var selectListBuff = new StringBuilder();
            var fromAndJoinsBuff = new StringBuilder();
            var whereConditionBuff = new StringBuilder();
            var splitOn = new StringBuilder();

            if (desiredNumberOfResults > 0)
            {
                //  I wouldn't normally do this with a non-parameterised value but you can't
                //  do SQL injection just using a positive integer.
                selectListBuff.Append("TOP (");
                selectListBuff.Append(desiredNumberOfResults);
                selectListBuff.Append(") ");
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

                var table = metadata.GetAttribute<TableAttribute>();

                if (index == 0)
                {
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
                    fromAndJoinsBuff.Append("LEFT OUTER JOIN ");
                    AppendTableNameAndAlias(fromAndJoinsBuff, table, alias);

                    AppendJoinCondition(map, index, entry, fromAndJoinsBuff, metadata, aliases);
                }
            }

            query.Sql = string.Format(@"SELECT {0}
{1}
{2};", selectListBuff, fromAndJoinsBuff, whereConditionBuff);
            query.SplitOn = splitOn.ToString();
            return query;
        }

        private static void AppendTableNameAndAlias(StringBuilder fromAndJoinsBuff, TableAttribute table, string alias)
        {
            fromAndJoinsBuff.Append(table.SchemaQualifiedTableName);
            fromAndJoinsBuff.Append(" AS ");
            fromAndJoinsBuff.Append(alias);
            fromAndJoinsBuff.Append(Environment.NewLine);
        }

        private static void AppendJoinCondition(TypePropertyMap map, int index, TypePropertyMapEntry entry,
            StringBuilder fromAndJoinsBuff, DtoMetadata metadata, string [] aliases)
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

            //  TODO: this is a bit naive for dealing with all the different relationship types
            //  the referenced PK might actually be on the parent object with the FK in the child.
            //  What about when the property is a collection that just has a [Column] attribute?
            //  This is definitely going Pete Tong.

            var targetProperty = target.GetPropertyMetadataFor(entry.Type);

            if (targetProperty.HasAttribute<OneToOneAttribute>() && string.IsNullOrEmpty(targetProperty.GetAttribute<OneToOneAttribute>().ChildForeignKeyColumn)
                || targetProperty.HasAttribute<ManyToOneAttribute>())
            {
                //  Covers situation where foreign key column is on the target table
            }
            else if (targetProperty.HasAttribute<OneToOneAttribute>() || targetProperty.HasAttribute<OneToManyAttribute>())
            {
                //  Covers situation where foreign key column is on the source table
            }
            else if (targetProperty.HasAttribute<ManyToManyAttribute>())
            {
                //  TODO: throw because we can't handle that here - we need to generate an extra JOIN that goes through the link table
            }
            else
            {
                //  TODO: throw because there's no indication of cardinality on the target property
            }

            fromAndJoinsBuff.Append("    ON ");
            AppendJoinConditionArgument(entry, fromAndJoinsBuff, metadata.PrimaryKey, aliases);
            fromAndJoinsBuff.Append(" = ");
            AppendJoinConditionArgument(target, fromAndJoinsBuff, targetProperty, aliases);
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
                        + "unlikely.",
                        entry.Type));
            }
            var alias = aliases == null ? entry.Alias : aliases[entry.Index];
            fromAndJoinsBuff.Append(alias);
            fromAndJoinsBuff.Append(".[");
            fromAndJoinsBuff.Append(property.ColumnName);
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
                            aliases);
                    }
                }
            }
        }

        private static void AppendConditionForParameter(
            StringBuilder whereConditionBuff,
            TypePropertyMapEntry entry,
            string parameterName,
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
            whereConditionBuff.Append("] = @");
            whereConditionBuff.Append(parameterName);
            whereConditionBuff.Append(Environment.NewLine);
        }
    }
}
