using System;
using System.Linq.Expressions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;


namespace QueryCraft.Operators.Filter
{
    public class EndsWithOperator : FilterOperator
    {
        public EndsWithOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : base(type, fieldName, value, converter)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Call(Property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Value);

            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
