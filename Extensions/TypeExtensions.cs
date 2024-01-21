using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryCraft.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Dictionary<Type, Func<string, object>> TypeConverters = new Dictionary<Type, Func<string, object>>()
        {
            { typeof(DateTime), value => DateTime.Parse(value) },
            { typeof(DateTime?), value => string.IsNullOrWhiteSpace(value) ? (DateTime?)null : DateTime.Parse(value) },
            { typeof(int), value => int.Parse(value) },
            { typeof(int?), value => string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value) },
            { typeof(bool), value => bool.Parse(value) },
            { typeof(bool?), value => string.IsNullOrWhiteSpace(value) ? (bool?)null : bool.Parse(value) },
            { typeof(double), value => double.Parse(value) },
            { typeof(double?), value => string.IsNullOrWhiteSpace(value) ? (double?)null : double.Parse(value) },
            { typeof(string), value => value },
            { typeof(Guid), value => Guid.Parse(value) },
            { typeof(Guid?), value => string.IsNullOrEmpty(value) ? (Guid?)null : Guid.Parse(value) } // add for custom types
        };

        public static Expression GetTypedValue(string Value, Type type)
        {
            if (TypeConverters.TryGetValue(type, out var converter))
            {
                return Expression.Constant(converter(Value), type);
            }
            return Expression.Constant(Convert.ChangeType(Value, type));
        }

        public static List<T> GetTypedList<T>(string value)
        {
            var elements = value.Trim('[', ']').Split(',');
            var list = new List<T>();
            foreach (var element in elements)
            {
                if (TypeConverters.TryGetValue(typeof(T), out var converter))
                {
                    list.Add((T)converter(element));
                }
                else
                {
                    list.Add((T)Convert.ChangeType(element, typeof(T)));
                }
            }
            return list;
        }

        public static T[] GetTypedTuple<T>(string value)
        {
            var elements = value.Trim('[', ']').Split(',');
            if (elements.Length != 2)
            {
                throw new InvalidOperationException("Between operator can only be used with two values.");
            }
            T[] array = new T[2];
            if (TypeConverters.TryGetValue(typeof(T), out var converter))
            {
                array[0] = (T)converter(elements[0]);
                array[1] = (T)converter(elements[1]);
            }
            else
            {
                array[0] = (T)Convert.ChangeType(elements[0], typeof(T));
                array[1] = (T)Convert.ChangeType(elements[1], typeof(T));

            }
            return array;
        }


        public static bool IsComparable(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) ||
                   type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    Nullable.GetUnderlyingType(type) != null &&
                    typeof(IComparable).IsAssignableFrom(Nullable.GetUnderlyingType(type));
        }
    }
}
