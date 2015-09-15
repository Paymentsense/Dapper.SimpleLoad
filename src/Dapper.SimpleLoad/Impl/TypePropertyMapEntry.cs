using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;
using Dapper.SimpleSave.Impl;

namespace Dapper.SimpleLoad.Impl
{
    public class TypePropertyMapEntry
    {
        private IDictionary<Type, PropertyMetadata> _typesToPropertyMetadata = new Dictionary<Type, PropertyMetadata>();

        public TypePropertyMapEntry(
            DtoMetadataCache cache,
            Type type,
            string alias,
            ISet<Type> typesWeCareAbout)
        {
            Type = type;
            Metadata = cache.GetMetadataFor(type);
            Alias = alias;
            foreach (var property in Metadata.Properties)
            {
                if (property.IsEnumerable)
                {
                    var genericArguments = property.Prop.PropertyType.GenericTypeArguments;
                    if (genericArguments == null
                        || genericArguments.Length != 1
                        || !typesWeCareAbout.Contains(genericArguments[0]))
                    {
                        continue;
                    }
                }
                else if (!typesWeCareAbout.Contains(property.Prop.PropertyType))
                {
                    continue;
                }

                _typesToPropertyMetadata[property.Prop.PropertyType] = property;
            }
        }

        public Type Type { get; private set; }

        public DtoMetadata Metadata { get; private set; }

        public string Alias { get; private set; }

        public PropertyMetadata GetPropertyMetadataFor(Type propertyType)
        {
            PropertyMetadata match;
            _typesToPropertyMetadata.TryGetValue(propertyType, out match);
            return match;
        }
    }
}
