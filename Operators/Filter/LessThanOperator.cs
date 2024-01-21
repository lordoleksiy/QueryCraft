using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class LessThanOperator : FilterOperator
    {
        public LessThanOperator(ParameterExpression type, string fieldName, string value) : base(type, fieldName, value)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.LessThan(Property, Value);
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
