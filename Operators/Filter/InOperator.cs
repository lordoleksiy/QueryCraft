using System;
using System.Linq.Expressions;
using QueryCraft.Extensions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;

namespace QueryCraft.Operators.Filter
{
    public class InOperator : FilterOperator
    {
        public InOperator(ParameterExpression type, string fieldName, string values, ITypeConverter converter) : base(type, fieldName)
        {
            Value = Expression.Constant(converter.GetTypedList(type.Type, values));
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Call(Value, "Contains", null, Property); // not contains
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
