using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Impl
{
    public static class QueryBuilder
    {
        public static string BuildQuery(TypePropertyMap map, object parameters)
        {
            var selectListBuff = new StringBuilder();
            var fromAndJoinsBuff = new StringBuilder();
            var whereConditionBuff = new StringBuilder();

            for (int index = 0, size = map.Count; index < size; ++index)
            {
                var entry = map[index];
                var metadata = entry.Metadata;
                SelectListBuilder.AppendSelectListFor(
                    selectListBuff,
                    metadata,
                    index > 0,
                    entry.Alias);

                var table = metadata.GetAttribute<TableAttribute>();

                if (index == 0)
                {
                    fromAndJoinsBuff.Append("FROM ");
                    fromAndJoinsBuff.Append(table.SchemaQualifiedTableName);
                    fromAndJoinsBuff.Append(Environment.NewLine);

                    BuildWhereCondition(parameters, whereConditionBuff, entry);
                }
                else
                {
                    fromAndJoinsBuff.Append("LEFT OUTER JOIN ");
                    fromAndJoinsBuff.Append(table.SchemaQualifiedTableName);
                    fromAndJoinsBuff.Append(Environment.NewLine);

                    AppendJoinCondition(map, index, entry, fromAndJoinsBuff, metadata);
                }
            }

            return string.Format(@"SELECT {0}
{1}
WHERE {2};", selectListBuff, fromAndJoinsBuff, whereConditionBuff);
        }

        private static void AppendJoinCondition(TypePropertyMap map, int index, TypePropertyMapEntry entry,
            StringBuilder fromAndJoinsBuff, DtoMetadata metadata)
        {
            var target = map.GetEntryWithMatchingPropertyPreceding(index, entry.Type);
            if (target == null)
            {
                //  TODO: throw an exception with a helpful message
            }

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
                //  TODO: throw an exception with a (different) helpful message
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
            if (whereConditionBuff.Length > 0)
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
