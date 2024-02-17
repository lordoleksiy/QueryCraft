using QueryCraft.Operators.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Linq.Expressions;
using QueryCraft.Extensions;
using QueryCraft.Interfaces;
using QueryCraft.Operators;

namespace QueryCraft.Parsing
{
    public class BodyParser: IParser
    {
        private ParameterExpression _typeExpression;
        private readonly IOperatorConverter _operatorConverter;

        public BodyParser(IOperatorConverter operatorConverter)
        {
            _operatorConverter = operatorConverter;
        }
        private IOperator ParseOperator(string Name, string body)
        {
            if (Name == "and")
            {
                if (TypeExtensions.TryGetListofDictionaries(body, out var andList))
                {
                    return new AndOperator(ParseList(andList), _typeExpression);
                }
                if (TypeExtensions.TryGetDictionary(body, out var andDict))
                {
                    return new AndOperator(ParseDictionary(andDict), _typeExpression);
                }
                throw new ArgumentException("Invalid value for operator 'and'");
            }
            else if (Name == "or")
            {
                if (TypeExtensions.TryGetListofDictionaries(body, out var orList))
                {
                    return new OrOperator(ParseList(orList), _typeExpression);
                }
                if (TypeExtensions.TryGetDictionary(body, out var orDict))
                {
                    return new OrOperator(ParseDictionary(orDict), _typeExpression);
                }
                throw new ArgumentException("Invalid value for operator 'or'");
            }
            else if (Name == "not")
            {
                if (TypeExtensions.TryGetDictionary(body, out var operandDict))
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

        public IOperator Parse(Dictionary<string, object> filterBody, Type type)
        {
            _typeExpression = Expression.Parameter(type, type.Name);
            if ( filterBody == null || !filterBody.Any())
            {
                return new TrueOperator(_typeExpression);
            }
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
            var _ = _typeExpression.Type.GetProperty(Name) ?? throw new ArgumentException($"Field with name {Name} doesn't exist in specified model.");

            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
            var value = jsonObject.First();
            return _operatorConverter.ConvertToFilterOperator(_typeExpression, value.Key, Name, value.Value.ToString());
        }
    }
}
