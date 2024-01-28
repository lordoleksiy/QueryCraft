using QueryCraft.Operators;
using System;
using System.Collections.Generic;

namespace QueryCraft.Interfaces
{
    public interface IParser
    {
        IOperator Parse(Dictionary<string, object> filterBody, Type type);
    }
}
