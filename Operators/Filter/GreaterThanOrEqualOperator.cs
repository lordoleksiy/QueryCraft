using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class GreaterThanOrEqualOperator : FilterOperator
    {
        public GreaterThanOrEqualOperator(ParameterExpression type, string fieldName, string value) : base(type, fieldName, value)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.GreaterThanOrEqual(Property, Value);
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
