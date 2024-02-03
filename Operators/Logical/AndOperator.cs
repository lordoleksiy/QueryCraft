using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QueryCraft.Operators;

namespace QueryCraft.Operators.Logical
{
    public class AndOperator : BaseOperator
    {
        public List<IOperator> Operators { get; set; }
        public AndOperator(List<IOperator> operators, ParameterExpression type) : base(type)
        {
            Operators = operators;
        }

        public override Expression<Func<T, bool>> GetPredicate<T>()
        {
            if (Operators == null || Operators.Count == 0)
            {
                throw new ArgumentException("AndOperator cannot be created with no child operators.");
            }

            Expression body = null;

            foreach (var op in Operators)
            {
                var opPredicate = op.GetPredicate<T>();
                if (body == null)
                {
                    body = opPredicate.Body;
                }
                else
                {
                    body = Expression.AndAlso(body, opPredicate.Body);
                }
            }

            if (body == null)
            {
                throw new InvalidOperationException("No predicates found in the Operators list.");
            }

            return Expression.Lambda<Func<T, bool>>(body, TypeExpression);
        }
    }

}
