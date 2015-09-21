using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.Impl
{
    public static class AliasGenerator
    {
        public static string GenerateAliasFor(Type type, int index)
        {
            return string.Format(
                "ta{0}_{1}",
                index,
                GenerateAliasFor(type));
        }

        public static string GenerateAliasFor(Type type)
        {
            return GenerateAliasFor(type.Name);
        }

        public static string GenerateAliasFor(string identifier, int index)
        {
            return string.Format(
                "ia{0}_{1}",
                index,
                GenerateAliasFor(identifier));
        }

        public static string GenerateAliasFor(string identifier)
        {
            char previous = (char) 0;
            var buff = new StringBuilder();
            foreach (char ch in identifier)
            {
                if (char.IsLetter(ch))
                {
                    if (buff.Length == 0 || char.IsUpper(ch) || ! char.IsLetter(previous))
                    {
                        buff.Append(char.ToLower(ch));
                    }
                }
                previous = ch;
            }
            return buff.ToString();
        }
    }
}
