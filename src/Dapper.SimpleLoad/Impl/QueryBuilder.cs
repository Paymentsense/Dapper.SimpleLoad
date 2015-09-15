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
            object parameters)
        {
            var query = new Query();
            var selectListBuff = new StringBuilder();
            var fromAndJoinsBuff = new StringBuilder();
            var whereConditionBuff = new StringBuilder();
            var splitOn = new StringBuilder();

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
                        BuildWhereCondition(parameters, whereConditionBuff, entry);
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

                    AppendJoinCondition(map, index, entry, fromAndJoinsBuff, metadata);
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
            StringBuilder fromAndJoinsBuff, DtoMetadata metadata)
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
            //  the referenced PK might actually be on the parent object with the FK in the child
            //  What about when the property is a collection that just has a [Column] attribute?
            //  This is definitely going Pete Tong.

            fromAndJoinsBuff.Append("    ON ");
            AppendJoinConditionArgument(entry, fromAndJoinsBuff, metadata.PrimaryKey);
            fromAndJoinsBuff.Append(" = ");
            AppendJoinConditionArgument(target, fromAndJoinsBuff, target.GetPropertyMetadataFor(entry.Type));
            fromAndJoinsBuff.Append(Environment.NewLine);
        }

        private static void AppendJoinConditionArgument(
            TypePropertyMapEntry entry,
            StringBuilder fromAndJoinsBuff,
            PropertyMetadata property)
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
            fromAndJoinsBuff.Append(entry.Alias);
            fromAndJoinsBuff.Append(".[");
            fromAndJoinsBuff.Append(property.ColumnName);
            fromAndJoinsBuff.Append("]");
        }

        private static void BuildWhereCondition(object parameters, StringBuilder whereConditionBuff, TypePropertyMapEntry entry)
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
                            kvp.Key);
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
                            property.Name);
                    }
                }
            }
        }

        private static void AppendConditionForParameter(StringBuilder whereConditionBuff, TypePropertyMapEntry entry,
            string parameterName)
        {
            if (whereConditionBuff.Length == 0)
            {
                whereConditionBuff.Append("WHERE ");
            }
            else
            {
                whereConditionBuff.Append("    AND ");
            }
            whereConditionBuff.Append(entry.Alias);
            whereConditionBuff.Append(".[");
            whereConditionBuff.Append(parameterName);
            whereConditionBuff.Append("] = @");
            whereConditionBuff.Append(parameterName);
            whereConditionBuff.Append(Environment.NewLine);
        }
    }
}
