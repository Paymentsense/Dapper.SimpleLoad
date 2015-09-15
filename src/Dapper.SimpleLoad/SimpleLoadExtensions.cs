using Dapper;
using System;
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

        public static IEnumerable<T1> AutoQuery<T1>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, DontMap, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, DontMap, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, DontMap, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, DontMap, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, T7, DontMap>(
                connection, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(
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

        public static IEnumerable<T1> AutoQuery<T1>(
            Type[] additionalTypes, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(additionalTypes, null, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, DontMap, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, DontMap, DontMap, DontMap, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, DontMap, DontMap, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, DontMap, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, DontMap, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1, T2, T3, T4, T5, T6, T7, DontMap>(connection, whereClauseExpression, parameters, transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IDbConnection connection, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            return AutoQuery<T1>(
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
                whereClauseExpression,
                parameters,
                transaction);
        }

        public static IEnumerable<T1> AutoQuery<T1>(
            Type[] additionalTypes, string whereClauseExpression, object parameters, IDbTransaction transaction = null)
        {
            var types = new List<Type>();
            types.Add(typeof(T1));
            types.AddRange(additionalTypes.TakeWhile(type => type != typeof (DontMap)));

            CheckTypes(types);

            var map = new TypePropertyMap(SimpleSaveExtensions.MetadataCache, types);

            var query = QueryBuilder.BuildQuery(map, parameters);


            throw new NotImplementedException();
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
    }
}
