using QueryCraft.Operators.Filter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;
using System.ComponentModel;

namespace QueryCraft.Convertors
{
    public class OperatorConverter: IOperatorConverter
    {
        private readonly ITypeConverter _typeConverter;
        public OperatorConverter(ITypeConverter typeConverter) 
        {
            _typeConverter = typeConverter;
        }
        public OperatorConverter(ITypeConverter typeConverter, Dictionary<string, Type> options)
        {
            _typeConverter = typeConverter;
            foreach (var key in options.Keys)
            {
                if (!options[key].IsSubclassOf(typeof(FilterOperator)))
                {
                    throw new ArgumentException($"The value associated with key '{key}' in the options dictionary must be a subclass of FilterOperator.");
                }
                if (operatorFactories.ContainsKey(key))
                {
                    operatorFactories[key] = (type, fieldName, value, converter) => Activator.CreateInstance(options[key], type, fieldName, value) as FilterOperator;
                }
                else
                {
                    operatorFactories.Add(key, (type, fieldName, value, converter) => Activator.CreateInstance(options[key], type, fieldName, value) as FilterOperator);
                }
            }
        }
        private readonly Dictionary<string, Func<ParameterExpression, string, string, ITypeConverter, FilterOperator>> operatorFactories = new Dictionary<string, Func<ParameterExpression, string, string, ITypeConverter, FilterOperator>>()
        {
            { "eq", (type, fieldName, value, typeConverter) => new EqualOperator(type, fieldName, value, typeConverter) },
            { "ne", (type, fieldName, value, typeConverter) => new NotEqualOperator(type, fieldName, value, typeConverter) },
            { "gt", (type, fieldName, value, typeConverter) => new GreaterThanOperator(type, fieldName, value, typeConverter) },
            { "lt", (type, fieldName, value, typeConverter) => new LessThanOperator(type, fieldName, value, typeConverter) },
            { "gte", (type, fieldName, value, typeConverter) => new GreaterThanOrEqualOperator(type, fieldName, value, typeConverter) },
            { "lte", (type, fieldName, value, typeConverter) => new LessThanOrEqualOperator(type, fieldName, value, typeConverter) },
            { "in", (type, fieldName, value, typeConverter) => new InOperator(type, fieldName, value, typeConverter) },
            { "nin", (type, fieldName, value, typeConverter) => new NotInOperator(type, fieldName, value, typeConverter) },
            { "startswith", (type, fieldName, value, typeConverter) => new StartsWithOperator(type, fieldName, value, typeConverter) },
            { "endswith", (type, fieldName, value, typeConverter) => new EndsWithOperator(type, fieldName, value, typeConverter) },
            { "between", (type, fieldName, value, typeConverter) => new BetweenOperator(type, fieldName, value, typeConverter) },
            { "isnull", (type, fieldName, value, typeConverter) => new IsNullOperator(type, fieldName, value, typeConverter) },
            { "contains", (type, fieldName, value, typeConverter) => new IsNullOperator(type, fieldName, value, typeConverter) },
        };

        public FilterOperator ConvertToFilterOperator(ParameterExpression type, string operat, string fieldName, string value)
        {
            if (!operatorFactories.TryGetValue(operat.ToLower().Replace(" ", ""), out var factory))
            {
                throw new ArgumentException($"Unknown operator: {operat}", nameof(operat));
            }

            return factory(type, fieldName, value, _typeConverter);
        }
    }
}
