using QueryCraft.Extensions;
using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class BetweenOperator : FilterOperator
    {
        private readonly object from;
        private readonly object to;
        public BetweenOperator(ParameterExpression type, string fieldName, string value) : base(type, fieldName)
        {
            var method = typeof(TypeExtensions).GetMethod("GetTypedTuple").MakeGenericMethod(Property.Type);
            var array = (Array)method.Invoke(null, new object[] { value });
            from = array.GetValue(0);
            to = array.GetValue(1);
        }

        public override Expression<Func<TEntity, bool>> GetPredicate<TEntity>()
        {
            Expression body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(Property, Expression.Constant(from, Property.Type)),
                Expression.LessThanOrEqual(Property, Expression.Constant(to, Property.Type))
            );

            return Expression.Lambda<Func<TEntity, bool>>(body, TypeExpression);
        }
    }
}
