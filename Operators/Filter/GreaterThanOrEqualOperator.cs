using System;
using System.Linq.Expressions;
using QueryCraft.Interfaces;

namespace QueryCraft.Operators.Filter
{
    public class GreaterThanOrEqualOperator : FilterOperator
    {
        public GreaterThanOrEqualOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : base(type, fieldName, value, converter)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.GreaterThanOrEqual(Property, Value);
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
