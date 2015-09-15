using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Impl
{
    public class TypePropertyMap
    {
        private readonly IList<TypePropertyMapEntry> _entries = new List<TypePropertyMapEntry>();

        public TypePropertyMap(
            DtoMetadataCache cache,
            IEnumerable<Type> types)
        {
            var typeSet = new HashSet<Type>();
            foreach (var type in types.Where(type => !typeSet.Contains(type)))
            {
                typeSet.Add(type);
            }

            int index = 0;
            foreach (var type in types)
            {
                _entries.Add(new TypePropertyMapEntry(
                    cache,
                    type,
                    GenerateAliasFor(type, index),
                    index,
                    typeSet));
                ++index;
            }
        }

        private string GenerateAliasFor(Type type, int count)
        {
            return string.Format(
                "a{0}_{1}",
                count,
                GenerateAliasFor(type));
        }

        private string GenerateAliasFor(Type type)
        {
            var buff = new StringBuilder();
            foreach (char ch in type.Name)
            {
                if (char.IsLetter(ch))
                {
                    if (buff.Length == 0 || char.IsUpper(ch))
                    {
                        buff.Append(char.ToLower(ch));
                    }
                }
            }
            return buff.ToString();
        }

        public TypePropertyMapEntry this[int index] { get { return _entries[index]; } }

        public int Count { get { return _entries.Count; } }

        public TypePropertyMapEntry GetEntryWithMatchingPropertyPreceding(int index, Type propertyType)
        {
            for (int currentIndex = index - 1; currentIndex >= 0; --currentIndex)
            {
                var entry = this[currentIndex];
                if (entry.GetPropertyMetadataFor(propertyType) != null)
                {
                    return entry;
                }
            }
            return null;
        }
    }
}
