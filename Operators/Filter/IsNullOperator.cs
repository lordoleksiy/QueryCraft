using System;
using System.Linq.Expressions;
using QueryCraft.Interfaces;

namespace QueryCraft.Operators.Filter
{
    public class IsNullOperator : FilterOperator
    {
        private readonly bool _isNull;

        public IsNullOperator(ParameterExpression type, string fieldName, string isNull, ITypeConverter converter) : base(type, fieldName, isNull, converter)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = _isNull ?
                Expression.Equal(Property, Expression.Constant(null, typeof(object))) :
                Expression.NotEqual(Property, Expression.Constant(null, typeof(object)));

            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
