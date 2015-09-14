using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using Dapper;
using Dapper.SimpleLoad.Impl;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.ReferenceData
{
    public class ReferenceDataService : BaseCustomQueryDapperRepository, IReferenceDataService
    {

        private const string BaseCacheKey = "PS.Mothership.Repository.Implementations.ReferenceDataRepository.";

        private readonly IDictionary<string, DtoMetadata> _referenceDataTypes
            = new Dictionary<string, DtoMetadata>(StringComparer.CurrentCultureIgnoreCase);

        private readonly ISet<string> _ambiguousTypeKeys
            = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);

        private readonly IDictionary<DtoMetadata, string> _metadataQueryCache
            = new Dictionary<DtoMetadata, string>();

        //  Note that this is only used for items where we need to hit the database - not for
        //  simple .NET enums.
        private readonly ObjectCache _cache;

        private readonly object _lock
            = new object();

        public ReferenceDataService(
            IDbConnectionFactory connectionFactory,
            ObjectCache cache,
            params Assembly[] assemblies)
            : base(connectionFactory)
        {
            _cache = cache;
            InitializeReferenceDataTypes(assemblies);
        }

        private void InitializeReferenceDataTypes(Assembly [] assemblies)
        {
            var keysToAllMatchingTypes = GenerateKeysToAllMatchingTypes(assemblies);
            PopulateReferenceTypesAndAmbiguousTypesMaps(keysToAllMatchingTypes);
        }

        private IDictionary<string, IList<DtoMetadata>> GenerateKeysToAllMatchingTypes(
            Assembly [] assemblies)
        {
            var keysToMetadata = new Dictionary<string, IList<DtoMetadata>>(StringComparer.CurrentCultureIgnoreCase);
            var metadataCache = new DtoMetadataCache();
            var seenYouBefore = new HashSet<Assembly>();

            foreach (var assembly in assemblies)
            {
                if (seenYouBefore.Contains(assembly))
                {
                    continue;
                }

                seenYouBefore.Add(assembly);

                foreach (var type in assembly.GetTypes())
                {
                    var metadata = metadataCache.GetMetadataFor(type);
                    if ((metadata.HasAttribute<TableAttribute>()
                        && metadata.HasAttribute<ReferenceDataAttribute>()
                        && !metadata
                            .GetAttribute<ReferenceDataAttribute>()
                            .HasUpdateableForeignKeys)
                        || type.IsEnum)
                    {
                        foreach (var typeKey in GetAllPossibleTypeKeys(metadata))
                        {
                            IList<DtoMetadata> matchingTypes;
                            keysToMetadata.TryGetValue(typeKey, out matchingTypes);
                            if (matchingTypes == null)
                            {
                                matchingTypes = new List<DtoMetadata>();
                                keysToMetadata.Add(typeKey, matchingTypes);
                            }
                            matchingTypes.Add(metadata);
                        }
                    }
                }
            }

            return keysToMetadata;
        }

        private IEnumerable<string> GetAllPossibleTypeKeys(DtoMetadata metadata)
        {
            var target = new List<string>();
            var name = metadata.DtoType.FullName;
            int matchIndex;
            do
            {
                target.Add(name);

                if (name.Length > 3 && name.EndsWith("Dto"))
                {
                    var temp = name.Substring(0, name.Length - 3);
                    target.Add(temp);
                }

                matchIndex = name.IndexOf('.');
                if (matchIndex >= 0)
                {
                    name = name.Substring(matchIndex + 1);
                }
            } while (matchIndex >= 0);

            return target;
        }

        private void PopulateReferenceTypesAndAmbiguousTypesMaps(IDictionary<string, IList<DtoMetadata>> keysToAllMatchingTypes)
        {
            foreach (var typeKey in keysToAllMatchingTypes.Keys)
            {
                var matches = keysToAllMatchingTypes[typeKey];
                if (matches.Count > 1)
                {
                    _ambiguousTypeKeys.Add(typeKey);
                }
                else
                {
                    _referenceDataTypes[typeKey] = matches.First();
                }
            }
        }

        public IEnumerable<dynamic> GetReferenceData(string typeKey)
        {
            return GetReferenceData<dynamic, object>(typeKey);
        }

        public IEnumerable<TResult> GetReferenceData<TResult>()
        {
            return GetReferenceData<TResult, object>(typeof (TResult).FullName);
        }

        public IEnumerable<dynamic> GetReferenceData<TColumnValue>(
            string typeKey,
            string columnName,
            TColumnValue matchingColumnValue)
        {
            return GetReferenceData<dynamic, TColumnValue>(typeKey, columnName, matchingColumnValue);
        }

        public IEnumerable<TResult> GetReferenceData<TResult, TColumnValue>(
            string columnName,
            TColumnValue matchingColumnValue)
        {
            return GetReferenceData<TResult, TColumnValue>(
                typeof(TResult).FullName,
                columnName,
                matchingColumnValue)
;        }

        private IEnumerable<TResult> GetReferenceData<TResult, TColumnValue>(
            string typeKey,
            string columnName = null,
            TColumnValue matchingColumnValue = default(TColumnValue))
        {
            DtoMetadata metadata;
            _referenceDataTypes.TryGetValue(typeKey, out metadata);

            if (metadata == null)
            {
                if (_ambiguousTypeKeys.Contains(typeKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "The type key '{0}' matches more than one reference data source. "
                            + "Please qualify your type key with an additional namespace prefix.",
                            typeKey),
                        "typeKey");
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        "typeKey",
                        typeKey,
                        string.Format("No such reference data for: {0}", typeKey));
                }
            }

            return metadata.DtoType.IsEnum
                ? (IEnumerable<TResult>) GetEnumReferenceData(metadata)
                : GetReferenceData<TResult, TColumnValue>(metadata, columnName, matchingColumnValue);
        }

        private IEnumerable<dynamic> GetEnumReferenceData(DtoMetadata metadata)
        {
            var target = new List<dynamic>();
            var enumType = metadata.DtoType;
            foreach (var value in Enum.GetValues(enumType))
            {
                var valueMember = enumType.GetMember(value.ToString());
                var attribute = valueMember[0].GetCustomAttributes(typeof (DescriptionAttribute)).FirstOrDefault() as DescriptionAttribute;
                var description = attribute != null
                    ? attribute.Description
                    : value.ToString();

                target.Add(new
                {
                    Value = value.ToString(),
                    Caption = string.IsNullOrEmpty(description) ? value.ToString() : description,
                    NumericValue = (int) value
                });
            }
            return target;
        }

        private IEnumerable<TResult> GetReferenceData<TResult, TColumnValue>(
            DtoMetadata metadata,
            string columnName = null,
            TColumnValue matchingColumnValue = default(TColumnValue))
        {
            var cacheKey = BaseCacheKey + metadata.DtoType.FullName;
            if (!string.IsNullOrEmpty(columnName))
            {
                cacheKey += "?" + columnName + "=" + matchingColumnValue;
            }

            var result = _cache.Get(cacheKey) as IEnumerable<TResult>;
            if (result == null)
            {
                var query = GetQueryFor(metadata, columnName);

                dynamic parameters = null;

                if (columnName != null)
                {
                    parameters = new ExpandoObject();
                    parameters.columnValue = matchingColumnValue;
                }

                result = Execute(connection => connection.Query<TResult>(query, (object) parameters));
                _cache.Set(
                    cacheKey,
                    result,
                    new CacheItemPolicy
                    {
                        AbsoluteExpiration = columnName == null
                            ? DateTimeOffset.Now.AddMinutes(10)
                            : DateTimeOffset.Now.AddMinutes(2)
                    });
            }
            return result;
        }

        private string GetQueryFor(
            DtoMetadata metadata,
            string whereClauseColumnName)
        {
            lock (_lock)
            {
                string query;
                _metadataQueryCache.TryGetValue(metadata, out query);
                if (query == null)
                {
                    query = BuildQueryFor(metadata);
                    _metadataQueryCache.Add(metadata, query);
                }

                return whereClauseColumnName == null
                    ? query + ";"
                    : query + string.Format(" WHERE [{0}] = @columnValue;", whereClauseColumnName);
            }
        }

        private string BuildQueryFor(DtoMetadata metadata)
        {
            var columns = SelectListBuilder.BuildSelectListFor(metadata);
            return string.Format(
                "SELECT {0} FROM {1}",
                columns,
                metadata.TableName);
        }
    }
}
