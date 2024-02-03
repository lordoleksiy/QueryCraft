using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators
{
    public abstract class BaseOperator : IOperator
    {
        protected BaseOperator(ParameterExpression type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            TypeExpression = type;
        }
        public ParameterExpression TypeExpression { get; set; }

        public abstract Expression<Func<T, bool>> GetPredicate<T>();
    }
}
