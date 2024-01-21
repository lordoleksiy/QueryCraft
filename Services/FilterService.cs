using System.Text.Json;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System;
using QueryCraft.Intrefaces;
using QueryCraft.Operators;
using QueryCraft.Operators.Logical;
using QueryCraft.Operators.Filter;

namespace QueryCraft.Services
{
    public class FilterService<T> : IFilterService<T> where T : class
    {
        private readonly ParameterExpression _typeExpression;
        public FilterService()
        {
            _typeExpression = Expression.Parameter(typeof(T), typeof(T).Name);
        }


        // Parsing
        public IOperator ParseOperator(string Name, string body)
        {
            if (Name == "and")
            {
                if (TryGetListofDictionaries(body, out var andList))
                {
                    return new AndOperator(ParseList(andList), _typeExpression);
                }
                if (TryGetDictionary(body, out var andDict))
                {
                    return new AndOperator(ParseDictionary(andDict), _typeExpression);
                }
                throw new ArgumentException("Invalid value for operator 'and'");
            }
            else if (Name == "or")
            {
                if (TryGetListofDictionaries(body, out var orList))
                {
                    return new OrOperator(ParseList(orList), _typeExpression);
                }
                if (TryGetDictionary(body, out var orDict))
                {
                    return new OrOperator(ParseDictionary(orDict), _typeExpression);
                }
                throw new ArgumentException("Invalid value for operator 'or'");
            }
            else if (Name == "not")
            {
                if (TryGetDictionary(body, out var operandDict))
                {
                    var childOperator = ParseDictionary(operandDict).FirstOrDefault();
                    if (childOperator != null)
                    {
                        return new NotOperator(childOperator, _typeExpression);
                    }
                }
                throw new ArgumentException("Invalid value for operator 'not'");
            }
            else
            {
                return ParseFilterOpetor(Name, body);
            }
        }
        public List<IOperator> ParseList(List<Dictionary<string, object>> operandList)
        {
            var operands = new List<IOperator>();
            foreach (var operandDict in operandList)
            {
                operands.AddRange(ParseDictionary(operandDict));
            }
            return operands;
        }
        public List<IOperator> ParseDictionary(Dictionary<string, object> operandDict)
        {
            var operands = new List<IOperator>();
            foreach (var operatorName in operandDict.Keys)
            {
                operands.Add(ParseOperator(operatorName, operandDict[operatorName].ToString()));
            }
            return operands;
        }

        public IOperator ParseFilter(Dictionary<string, object> filterBody)
        {
            if (filterBody.Count < 1) return null;

            if (filterBody.Count > 1)
            {
                return new AndOperator(ParseDictionary(filterBody), _typeExpression);
            }
            var filter = filterBody.First();
            return ParseOperator(filter.Key, filter.Value.ToString());
        }

        public FilterOperator ParseFilterOpetor(string Name, string body)
        {
            var type = typeof(T);
            var _ = type.GetProperty(Name) ?? throw new ArgumentException($"Field with name {Name} doesn't exist in specified model.");

            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
            var value = jsonObject.First();
            return ConvertToFilterOperator(value.Key, Name, value.Value.ToString());
        }


        // factory method
        public FilterOperator ConvertToFilterOperator(string operat, string fieldName, string value)
        {
            switch (operat.ToLower().Replace(" ", ""))
            {
                case "eq":
                    return new EqualOperator(_typeExpression, fieldName, value);
                case "ne":
                    return new NotEqualOperator(_typeExpression, fieldName, value);
                case "gt":
                    return new GreaterThanOperator(_typeExpression, fieldName, value);
                case "lt":
                    return new LessThanOperator(_typeExpression, fieldName, value);
                case "gte":
                    return new GreaterThanOrEqualOperator(_typeExpression, fieldName, value);
                case "lte":
                    return new LessThanOrEqualOperator(_typeExpression, fieldName, value);
                case "in":
                    return new InOperator(_typeExpression, fieldName, value);
                case "nin":
                    return new NotInOperator(_typeExpression, fieldName, value);
                case "startswith":
                    return new StartsWithOperator(_typeExpression, fieldName, value);
                case "endswith":
                    return new EndsWithOperator(_typeExpression, fieldName, value);
                case "between":
                    return new BetweenOperator(_typeExpression, fieldName, value);
                case "isnull":
                    return new IsNullOperator(_typeExpression, fieldName, value);
                default:
                    throw new ArgumentException($"Unknown filter operator: {operat}", nameof(value));
            }
        }


        //Helpers
        public static bool TryGetDictionary(string value, out Dictionary<string, object> operandDict)
        {
            try
            {
                operandDict = JsonSerializer.Deserialize<Dictionary<string, object>>(value);
                return true;
            }
            catch (JsonException)
            {
                operandDict = null;
                return false;
            }
        }

        public static bool TryGetListofDictionaries(string value, out List<Dictionary<string, object>> listOfOperands)
        {
            try
            {
                listOfOperands = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value);
                return true;
            }
            catch (JsonException)
            {
                listOfOperands = null;
                return false;
            }
        }
    }
}
