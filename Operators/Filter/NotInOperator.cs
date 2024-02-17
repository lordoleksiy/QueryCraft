using QueryCraft.Extensions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;
using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class NotInOperator : FilterOperator
    {
        public NotInOperator(ParameterExpression type, string fieldName, string values, ITypeConverter converter) : base(type, fieldName)
        {
            Value = Expression.Constant(converter.GetTypedList(type.Type, values));
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Not(Expression.Call(Value, "Contains", null, Property));
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
