using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryCraft.Interfaces
{
    public interface ITypeConverter
    {
        object GetTypedValue(string value, Type type);
        Expression GetTypedValueExpression(string value, Type type);
        List<object> GetTypedList(Type type, string value);
        Array GetTypedArray(Type type, string value);
    }
}
