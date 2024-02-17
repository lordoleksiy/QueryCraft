using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCraft.Convertors
{
    internal class GenericTypeConverter
    {
        public GenericTypeConverter() { }
        public GenericTypeConverter(Dictionary<Type, Func<string, object>> options)
        {
            foreach (var key in options.Keys)
            {
                if (Converters.ContainsKey(key))
                {
                    Converters[key] = options[key];
                }
                else
                {
                    Converters.Add(key, options[key]);
                }
            }
        }
        private readonly Dictionary<Type, Func<string, object>> Converters = new Dictionary<Type, Func<string, object>>()
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
            { typeof(Guid?), value => string.IsNullOrEmpty(value) ? (Guid?)null : Guid.Parse(value) }
        };
        public T GetTypedValue<T>(string value, Type type = null)
        {
            type = type ?? typeof(T);
            if (Converters.TryGetValue(type, out var converter))
            {
                return (T)converter(value);
            }
            return (T)Convert.ChangeType(value, type);
        }

        public List<T> GetTypedList<T>(string value)
        {
            var elements = value.Trim('[', ']').Split(',');
            var list = new List<T>();
            foreach (var element in elements)
            {
                list.Add(GetTypedValue<T>(element));
            }
            return list;
        }

        public T[] GetTypedArray<T>(string value)
        {
            var elements = value.Trim('[', ']').Split(',');
            T[] array = new T[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {

                array[i] = GetTypedValue<T>(elements[i]);
            }
            return array;
        }
    }
}
