using System;
using System.Collections.Generic;
using System.Text.Json;

namespace QueryCraft.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsComparable(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) ||
                   type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    Nullable.GetUnderlyingType(type) != null &&
                    typeof(IComparable).IsAssignableFrom(Nullable.GetUnderlyingType(type));
        }

        public static bool TryGetDictionary(string value, out Dictionary<string, object> operandDict)
        {
            try
            {
                operandDict = JsonSerializer.Deserialize<Dictionary<string, object>>(value);
                return true;
            }
            catch (JsonException)
            {
                operandDict = null;
                return false;
            }
        }

        public static bool TryGetListofDictionaries(string value, out List<Dictionary<string, object>> listOfOperands)
        {
            try
            {
                listOfOperands = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value);
                return true;
            }
            catch (JsonException)
            {
                listOfOperands = null;
                return false;
            }
        }
    }
}
