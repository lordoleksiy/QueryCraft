using System;
using System.Linq.Expressions;
using QueryCraft.Interfaces;

namespace QueryCraft.Operators.Filter
{
    public class StartsWithOperator : FilterOperator
    {
        public StartsWithOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : base(type, fieldName, value, converter)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Call(Property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Value);

            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
