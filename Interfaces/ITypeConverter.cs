using System;
using System.Linq.Expressions;

namespace QueryCraft.Interfaces
{
    public interface ITypeConverter
    {
        Expression GetTypedValueExpression(string value, Type type);
        T GetTypedValue<T>(string value, Type type = null);
    }
}
