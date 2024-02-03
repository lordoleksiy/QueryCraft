using System;
using System.Linq.Expressions;
using QueryCraft.Extensions;
using QueryCraft.Interfaces;

namespace QueryCraft.Operators
{
    public abstract class FilterOperator : BaseOperator
    {
        public MemberExpression Property { get; set; }
        public Expression Value { get; set; }
        public override abstract Expression<Func<T, bool>> GetPredicate<T>();
        protected FilterOperator(ParameterExpression type, string fieldName) : base(type)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            }

            Property = Expression.Property(type, fieldName);
        }
        public FilterOperator(ParameterExpression type, string fieldName, string value, ITypeConverter converter) : this(type, fieldName)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            }

            Value = converter.GetTypedValueExpression(value, Property.Type);
        }
    }
}
