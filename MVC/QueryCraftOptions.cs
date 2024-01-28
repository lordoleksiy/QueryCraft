using System;
using System.Collections.Generic;

namespace QueryCraft.MVC
{
    public class QueryCraftOptions
    {
        public Dictionary<Type, Func<string, object>> ConverterTypes { get; } = new Dictionary<Type, Func<string, object>>();
        public Dictionary<string, Type> ConverterOperators { get; } = new Dictionary<string, Type>();
    }
}
