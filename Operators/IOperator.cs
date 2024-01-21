using System;
using System.Linq.Expressions;

namespace QueryCraft.Operators
{
    public interface IOperator
    {
        ParameterExpression TypeExpression { get; set; }
        Expression<Func<T, bool>> GetPredicate<T>();
    }
}
