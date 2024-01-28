using QueryCraft.Operators;
using System.Linq.Expressions;

namespace QueryCraft.Interfaces
{
    public interface IOperatorConverter
    {
        FilterOperator ConvertToFilterOperator(ParameterExpression type, string operat, string fieldName, string value);
    }
}
