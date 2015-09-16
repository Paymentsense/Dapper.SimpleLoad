using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleLoad.Impl;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad
{
    public static class SimpleLoadExtensions
    {
        private class DontMap {}

        public static IList<T1> AutoQuery<T1>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, T7, DontMap>(
                connection, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(
                connection,
                new[]
                {
                    typeof (T2),
                    typeof (T3),
                    typeof (T4),
                    typeof (T5),
                    typeof (T6),
                    typeof (T7),
                    typeof (T8)
                },
                parameters,
                transaction);
        }

        public static IList<T1> AutoQuery<T1>(
            this IDbConnection connection, Type[] additionalTypes, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(connection, additionalTypes, null, null, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, DontMap, DontMap, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, DontMap, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, DontMap, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, T7, DontMap>(connection, tableAliases, whereClauseExpression, parameters, transaction);
        }

        public static IList<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IDbConnection connection, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(
                connection,
                new[]
                {
                    typeof (T2),
                    typeof (T3),
                    typeof (T4),
                    typeof (T5),
                    typeof (T6),
                    typeof (T7),
                    typeof (T8)
                },
                tableAliases,
                whereClauseExpression,
                parameters,
                transaction);
        }

        public static IList<T1> AutoQuery<T1>(
            this IDbConnection connection, Type[] additionalTypes, string [] tableAliases, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            var types = new List<Type>();
            types.Add(typeof(T1));
            types.AddRange(additionalTypes.TakeWhile(type => type != typeof (DontMap)));

            CheckTypes(types);
            CheckTableAliases(types, tableAliases, whereClauseExpression);

            return AutoQuery<T1>(connection, types, tableAliases, whereClauseExpression, parameters);
        }

        private static IList<T1> AutoQuery<T1>(IDbConnection connection, List<Type> types, string[] tableAliases,
            string whereClauseExpression, object parameters)
        {
            var map = new TypePropertyMap(SimpleSaveExtensions.MetadataCache, types);
            var query = QueryBuilder.BuildQuery(map, tableAliases, whereClauseExpression, parameters);
            var alreadyEncounteredDictionaries = CreateAlreadyEncounteredDictionaries(types.Count);
            var results = new List<T1>();

            connection.Query(
                query.Sql,
                types.ToArray(),
                objects =>
                {
                    for (int index = 0, size = objects.Length; index < size; ++index)
                    {
                        var current = objects[index];
                        var alreadyEncountered = alreadyEncounteredDictionaries[index];
                        var entry = map[index];
                        var metadata = entry.Metadata;
                        var primaryKey = metadata.GetPrimaryKeyValueAsObject(current);
                        if (alreadyEncountered.Contains(primaryKey))
                        {
                            current = alreadyEncountered[primaryKey];
                        }
                        else
                        {
                            alreadyEncountered[primaryKey] = current;
                            if (index == 0)
                            {
                                results.Add((T1) current);
                            }
                        }

                        if (index > 0)
                        {
                            var targetEntry = map.GetEntryWithMatchingPropertyPreceding(index, entry.Type);
                            var propertyMetadata = targetEntry.GetPropertyMetadataFor(entry.Type);
                            var targetObject = objects[targetEntry.Index];
                            var targetsAlreadyEncountered = alreadyEncounteredDictionaries[targetEntry.Index];
                            var targetPrimaryKey = targetEntry.Metadata.GetPrimaryKeyValueAsObject(targetObject);
                            if (targetsAlreadyEncountered.Contains(targetObject))
                            {
                                targetObject = targetsAlreadyEncountered[targetPrimaryKey];
                            }

                            if (propertyMetadata.Prop.PropertyType == entry.Type)
                            {
                                propertyMetadata.Prop.SetValue(targetObject, current);
                            }
                            else if (propertyMetadata.IsEnumerable)
                            {
                                var list = propertyMetadata.Prop.GetValue(targetObject);
                                if (null == list)
                                {
                                    list = Activator.CreateInstance(propertyMetadata.Prop.PropertyType);
                                    propertyMetadata.Prop.SetValue(targetObject, list);
                                }

                                var addMethod = propertyMetadata.Prop.PropertyType.GetMethod("Add");
                                if (addMethod == null)
                                {
                                    throw new InvalidOperationException(
                                        string.Format(
                                            "The type '{0}' of property '{1}' on '{2}' does not implement an "
                                            + "Add(object) method so objects of type '{3}' cannot be added to it.",
                                            propertyMetadata.Prop.PropertyType.FullName,
                                            propertyMetadata.Prop.Name,
                                            targetEntry.Metadata.DtoType.FullName,
                                            metadata.DtoType.FullName));
                                }

                                addMethod.Invoke(list, new [] {current});
                            }
                            else
                            {
                                throw new InvalidOperationException(
                                    string.Format(
                                        "The property '{0}' on '{1}' is of type '{2}', which does not match "
                                        + "the type of value to be set, which is a '{3}', nor is it a list to "
                                        + "which the '{3}' can be added",
                                        propertyMetadata.Prop.Name,
                                        targetEntry.Metadata.DtoType.FullName,
                                        propertyMetadata.Prop.PropertyType.FullName,
                                        metadata.DtoType.FullName));
                            }
                        }
                    }
                    return true;
                },
                splitOn: query.SplitOn,
                param: parameters);

            return results;
        }

        private static IList<Hashtable> CreateAlreadyEncounteredDictionaries(int count)
        {
            var alreadyEncounteredDictionaries = new List<Hashtable>(count);
            for (int index = 0, size = count; index < size; ++index)
            {
                alreadyEncounteredDictionaries.Add(new Hashtable());
            }
            return alreadyEncounteredDictionaries;
        }

        private static void CheckTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                CheckType(type);
            }
        }

        private static void CheckType(Type type)
        {
            var metadata = SimpleSaveExtensions.MetadataCache.GetMetadataFor(type);
            if (!metadata.HasAttribute<TableAttribute>())
            {
                throw new ArgumentException(string.Format(
                    "Type '{0}' has not been decorated with a [Table(qualifiedTableName)] attribute. "
                    + "You must add this attribute in order for SimpleLoad to know which table to load data from.",
                    type.FullName));
            }
        }

        private static void CheckTableAliases(
            IList<Type> types, string[] tableAliases, string whereClauseExpression)
        {
            if (string.IsNullOrEmpty(whereClauseExpression))
            {
                return;
            }

            if (tableAliases == null || tableAliases.Length != types.Count)
            {
                throw new ArgumentException(
                    string.Format(
                        "If you specify a WHERE clause expression you must also supply a list of table aliases to be used in the query. "
                        + "The number of table aliases should match the number of types you have included in the query. In this case you "
                        + "have specified {0} types but {1} table aliases.",
                        types.Count,
                        null == tableAliases ? 0 : tableAliases.Length),
                    "tableAliases");
            }

            var alreadySeen = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            int index = 0;
            foreach (var alias in tableAliases)
            {
                var trimmed = alias;
                if (trimmed != null)
                {
                    trimmed = trimmed.Trim();
                }

                if (string.IsNullOrEmpty(trimmed))
                {
                    throw new ArgumentException(
                        string.Format(
                            "All table aliases must be valid T-SQL identifiers* and cannot be null or empty. Alias at index {0} "
                            + "is null or empty. *No, if you're the kind of person who likes to put single quotes or other random "
                            + "but still for whatever reason legal characters into SQL identifiers, this isn't going to work: keep "
                            + "things alphanumeric please.",
                            index),
                        "tableAliases");
                }

                if (alreadySeen.Contains(trimmed))
                {
                    throw new ArgumentException(
                        string.Format(
                            "Table aliases must be unique within the context of a single auto-query. The alias '{0}' is used "
                            + "more than once. Please choose different aliases for any duplicate uses and ensure you update "
                            + "your WHERE clause condition accordingly.",
                            trimmed),
                        "tableAliases");
                }

                if (char.IsDigit(trimmed[0]))
                {
                    throw new ArgumentException(
                        string.Format(
                            "Table aliases must not start with a digit. The alias '{0}' is invalid because it starts with a "
                            + "digit. Please remove the digit or prepend one or more letters.",
                            trimmed),
                        "tableAliases");
                }

                alreadySeen.Add(trimmed);
                ++index;
            }
        }
    }
}
