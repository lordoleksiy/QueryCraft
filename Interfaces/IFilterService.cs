using QueryCraft.Operators;
using System.Collections.Generic;

namespace QueryCraft.Intrefaces
{
    public interface IFilterService<T>
    {
        IOperator ParseFilter(Dictionary<string, object> filterBody);
    }
}
