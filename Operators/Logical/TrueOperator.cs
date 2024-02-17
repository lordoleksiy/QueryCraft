using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Logical
{
    public class TrueOperator : BaseOperator
    {
        public TrueOperator(ParameterExpression type) : base(type)
        {
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            return TypeExpression => true;
        }
    }
}
