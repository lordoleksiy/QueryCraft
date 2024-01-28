using QueryCraft.Operators.Filter;
using QueryCraft.Operators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QueryCraft.Interfaces;

namespace QueryCraft.Convertors
{
    public class OperatorConverter: IOperatorConverter
    {
        public OperatorConverter() { }
        public OperatorConverter(Dictionary<string, Type> options)
        {
            foreach (var key in options.Keys)
            {
                if (!options[key].IsSubclassOf(typeof(FilterOperator)))
                {
                    throw new ArgumentException($"The value associated with key '{key}' in the options dictionary must be a subclass of FilterOperator.");
                }
                if (operatorFactories.ContainsKey(key))
                {
                    operatorFactories[key] = (type, fieldName, value) => Activator.CreateInstance(options[key], type, fieldName, value) as FilterOperator;
                }
                else
                {
                    operatorFactories.Add(key, (type, fieldName, value) => Activator.CreateInstance(options[key], type, fieldName, value) as FilterOperator);
                }
            }
        }
        private readonly Dictionary<string, Func<ParameterExpression, string, string, FilterOperator>> operatorFactories = new Dictionary<string, Func<ParameterExpression, string, string, FilterOperator>>()
        {
            { "eq", (type, fieldName, value) => new EqualOperator(type, fieldName, value) },
            { "ne", (type, fieldName, value) => new NotEqualOperator(type, fieldName, value) },
            { "gt", (type, fieldName, value) => new GreaterThanOperator(type, fieldName, value) },
            { "lt", (type, fieldName, value) => new LessThanOperator(type, fieldName, value) },
            { "gte", (type, fieldName, value) => new GreaterThanOrEqualOperator(type, fieldName, value) },
            { "lte", (type, fieldName, value) => new LessThanOrEqualOperator(type, fieldName, value) },
            { "in", (type, fieldName, value) => new InOperator(type, fieldName, value) },
            { "nin", (type, fieldName, value) => new NotInOperator(type, fieldName, value) },
            { "startswith", (type, fieldName, value) => new StartsWithOperator(type, fieldName, value) },
            { "endswith", (type, fieldName, value) => new EndsWithOperator(type, fieldName, value) },
            { "between", (type, fieldName, value) => new BetweenOperator(type, fieldName, value) },
            { "isnull", (type, fieldName, value) => new IsNullOperator(type, fieldName, value) }
        };

        public FilterOperator ConvertToFilterOperator(ParameterExpression type, string operat, string fieldName, string value)
        {
            if (!operatorFactories.TryGetValue(operat.ToLower().Replace(" ", ""), out var factory))
            {
                throw new ArgumentException($"Unknown operator: {operat}", nameof(operat));
            }

            return factory(type, fieldName, value);
        }
    }
}
