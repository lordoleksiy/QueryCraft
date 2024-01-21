using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class IsNullOperator : FilterOperator
    {
        private readonly bool _isNull;

        public IsNullOperator(ParameterExpression type, string fieldName, string isNull) : base(type, fieldName)
        {
            if (!bool.TryParse(isNull, out _isNull))
            {
                _isNull = true;
            }
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
