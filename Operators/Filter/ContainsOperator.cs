using QueryCraft.Interfaces;
using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class ContainsOperator : FilterOperator
    {
        public ContainsOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : base(type, fieldName, value, converter)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Call(Property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Value);

            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
