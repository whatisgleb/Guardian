using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;
using DynamicExpression = System.Linq.Dynamic.DynamicExpression;

namespace Guardian.Library
{
    public class Validator
    {
        private IPrefixConverter _prefixConverter;

        public Validator(IPrefixConverter prefixConverter) {
            
            _prefixConverter = prefixConverter;
        }

        public List<ValidationError> Validate(object target, IEnumerable<IRuleGroup> ruleGroups, IEnumerable<IRule> rules) {

            List<ValidationError> errors = new List<ValidationError>();
            Dictionary<int, bool> results = new Dictionary<int, bool>();

            // Evaluate rule groups
            foreach (var ruleGroup in ruleGroups) {

                Stack<Operator> operatorStack = new Stack<Operator>();
                Stack<bool> valueStack = new Stack<bool>();
                Stack<Token> tokenStack = _prefixConverter.ConvertToStack(ruleGroup.Expression);

                // Iterate through stack
                while (tokenStack.Any()) {

                    Token token = tokenStack.Pop();

                    if (token.IsOperator()) {
                        
                        operatorStack.Push((Operator)token);
                    }

                    if (token.IsIdentifier()) {

                        Identifier identifier = (Identifier) token;

                        IRule rule = rules.FirstOrDefault(r => r.ID == identifier.ID);
                        bool ruleValue = results.ContainsKey(rule.ID) ? results[rule.ID] : EvaluateRule(target, rule);

                        bool left;
                        bool? right = null;

                        if (valueStack.Any()) {

                            left = valueStack.Pop();
                            right = ruleValue;
                        }
                        else {

                            left = ruleValue;
                        }

                        if (!operatorStack.Any()) {

                            if (tokenStack.Any()) {
                                
                                throw new Exception($"Unable to validate. Found an unexpected mismatch in the number of operands vs operators in expression, '{ruleGroup.Expression}'.");
                            }

                            valueStack.Push(left);
                            break;
                        }

                        if (!right.HasValue && operatorStack.Peek().Type == OperatorTypeEnum.Not) {
                            
                            // Remove the NOT operator from stack
                            operatorStack.Pop();

                            left = !left;

                            // Evaluate and store
                            valueStack.Push(left);
                        }

                        // Can we opt out?
                        if (left == false && operatorStack.Peek().Type == OperatorTypeEnum.And)
                        {
                            // false && anything => false => opt out
                            valueStack.Push(false);
                            break;
                        }

                        if (left == true && operatorStack.Peek().Type == OperatorTypeEnum.Or)
                        {
                            // true || anything => true => opt out
                            valueStack.Push(true);
                            break;
                        }

                        if (right.HasValue) {

                            operatorStack.Pop();
                            valueStack.Push(right.Value);
                        }
                    }
                }

                bool outcome = valueStack.Pop();

                if (outcome)
                {
                    errors.Add(new ValidationError()
                    {
                        ErrorMessage = ruleGroup.ErrorMessage,
                        Key = ruleGroup.Key
                    });
                }
            }

            return errors;
        }

        private bool EvaluateRule(object target, IRule rule) {

            var parameter = Expression.Parameter(target.GetType(), target.GetType().Name);
            var expression = DynamicExpression.ParseLambda(new[] { parameter }, typeof(bool), rule.Expression);

            return (bool)expression.Compile().DynamicInvoke(target);
        }
    }
}
