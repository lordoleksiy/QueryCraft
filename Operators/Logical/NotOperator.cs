using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators.Logical
{
    public class NotOperator : BaseOperator
    {
        public IOperator Operand { get; set; }
        public NotOperator(IOperator operand, ParameterExpression type) : base(type)
        {
            Operand = operand;
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            var operandPredicate = Operand.GetPredicate<T>();
            var notPredicate = Expression.Lambda<Func<T, bool>>(
                Expression.Not(operandPredicate.Body),
                TypeExpression);

            return notPredicate;
        }
    }
}
