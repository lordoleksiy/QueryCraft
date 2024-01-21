using System;
using System.Linq.Expressions;
using QueryCraft.Extensions;

namespace QueryCraft.Operators
{
    public abstract class FilterOperator : BaseOperator
    {
        public MemberExpression Property { get; set; }
        public Expression Value { get; set; }
        public override abstract Expression<Func<T, bool>> GetPredicate<T>();
        protected FilterOperator(ParameterExpression type, string fieldName) : base(type)
        {
            Property = Expression.Property(type, fieldName);
        }
        public FilterOperator(ParameterExpression type, string fieldName, string value) : this(type, fieldName)
        {
            Value = TypeExtensions.GetTypedValue(value, Property.Type);
        }
    }
}
