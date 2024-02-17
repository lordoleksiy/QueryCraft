using QueryCraft.Extensions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;
using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class BetweenOperator : FilterOperator
    {
        private readonly object from;
        private readonly object to;
        public BetweenOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : base(type, fieldName)
        {
            var array = converter.GetTypedArray(Property.Type, value);
            if (array.Length != 2)
            {
                throw new ArgumentException("The value string should represent an array with exactly two elements.", nameof(value));
            }

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
