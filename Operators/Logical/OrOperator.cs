using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QueryCraft.Operators;

namespace QueryCraft.Operators.Logical
{
    public class OrOperator : BaseOperator
    {
        public List<IOperator> Operators { get; set; }
        public OrOperator(List<IOperator> operators, ParameterExpression type) : base(type)
        {
            Operators = operators;
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            if (Operators == null || Operators.Count == 0)
            {
                throw new ArgumentException("Or cannot be created with no child operators.");
            }
            Expression<Func<T, bool>> combinedPredicate = null;
            foreach (var op in Operators)
            {
                if (combinedPredicate == null)
                {
                    combinedPredicate = op.GetPredicate<T>();
                }
                else
                {
                    var nextPredicate = op.GetPredicate<T>();
                    var body = Expression.OrElse(
                        Expression.Invoke(combinedPredicate, TypeExpression),
                        Expression.Invoke(nextPredicate, TypeExpression)
                    );
                    combinedPredicate = Expression.Lambda<Func<T, bool>>(body, TypeExpression);
                }
            }

            return combinedPredicate;
        }
    }
}
