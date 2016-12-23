using System;

namespace Dapper.SimpleLoad
{
    public static class Preconditions
    {
        public static void IsGreaterThanOrEqualTo<T>(this T value, string name, T threshold)
            where T: IComparable
        {
            if (value.CompareTo(threshold) < 0)
            {
                throw new ArgumentOutOfRangeException(
                    name,
                    value,
                    $"Invalid value: {value}. Value of {name} must be greater than {threshold}.");
            }
        }
    }
}
