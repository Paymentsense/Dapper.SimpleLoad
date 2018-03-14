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
            int index,
            ISet<Type> typesWeCareAbout,
            IEnumerable<Type> types)
        {
            Type = type;
            Metadata = cache.GetMetadataFor(type);
            Alias = alias;
            Index = index;
            foreach (var property in Metadata.AllProperties)
            {
                if (property.HasAttribute<SimpleLoadIgnoreAttribute>())
                {
                    continue;
                }

                var key = property.Prop.PropertyType;
                if (property.IsEnumerable)
                {
                    var genericArguments = property.Prop.PropertyType.GenericTypeArguments;
                    if (genericArguments == null
                        || genericArguments.Length != 1
                        || !typesWeCareAbout.Contains(genericArguments[0])
                        || !HasCardinalityAttribute(property))
                    {
                        continue;
                    }

                    key = genericArguments[0];
                }
                else if (!typesWeCareAbout.Contains(key))
                {
                    if (property.HasAttribute<ForeignKeyReferenceAttribute>())
                    {
                        //  Value type back references are the only property types we support. If there's any kind of object
                        //  in there we ignore it for the purpose of back referencing. Basically, if something's marked with
                        //  one of SimpleSave's cardinality attributes we assume it's a forward reference; if it's not marked
                        //  with such a reference and it's not a value type then it's also not a supported foreign key column
                        //  type, so we can't do anything with it anyway.
                        if (!property.IsValueType && property.IsReferenceType)
                        {
                            continue;
                        }

                        var foreignKey = property.GetAttribute<ForeignKeyReferenceAttribute>();
                        key = foreignKey.ReferencedDto;

                        if (!typesWeCareAbout.Contains(key))
                        {
                            key = types.Take(index).Reverse().FirstOrDefault(t => key.IsAssignableFrom(t));

                            if (key == null)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (!HasCardinalityAttribute(property) && !property.IsEnum)
                {
                    continue;
                }

                _typesToPropertyMetadata[key] = property;
            }
        }

        private bool HasCardinalityAttribute(PropertyMetadata propertyMetadata)
        {
            return propertyMetadata.IsOneToManyRelationship
                || propertyMetadata.IsManyToOneRelationship
                || propertyMetadata.IsOneToOneRelationship
                || propertyMetadata.IsManyToManyRelationship;
        }

        public Type Type { get; private set; }

        public DtoMetadata Metadata { get; private set; }

        public string Alias { get; private set; }

        public int Index { get; private set; }

        public PropertyMetadata GetPropertyMetadataFor(Type propertyType)
        {
            PropertyMetadata match;
            _typesToPropertyMetadata.TryGetValue(propertyType, out match);
            return match;
        }
    }
}
