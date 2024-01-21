using QueryCraft.Extensions;
using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Filter
{
    public class NotInOperator : FilterOperator
    {
        public NotInOperator(ParameterExpression type, string fieldName, string values) : base(type, fieldName)
        {
            var method = typeof(TypeExtensions).GetMethod("GetTypedList").MakeGenericMethod(Property.Type);
            Value = Expression.Constant(method.Invoke(null, new object[] { values }));
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            Expression body = Expression.Not(Expression.Call(Value, "Contains", null, Property)); // not contains
            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }
}
