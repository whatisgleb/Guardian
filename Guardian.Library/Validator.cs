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
using Guardian.Library.Tokens.Values;
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

                Stack<Token> outputStack = new Stack<Token>();
                Stack<Token> tokenStack = _prefixConverter.ConvertToStack(ruleGroup.Expression);

                // Reverse stack
                Stack<Token> reverseStack = new Stack<Token>(tokenStack);

                // Iterate through stack
                while (reverseStack.Any()) {
                    
                    Token token = reverseStack.Pop();

                    if (token.IsIdentifierToken()) {
                        
                        outputStack.Push((IdentifierToken)token);
                    }

                    if (token.IsOperatorToken()) {

                        OperatorToken op = (OperatorToken) token;
                        Token leftOutputToken = outputStack.Pop();
                        bool left;

                        if (leftOutputToken.IsValueToken()) {

                            left = ((ValueToken) leftOutputToken).Value;
                        }
                        else {

                            IdentifierToken leftIdentifier = (IdentifierToken) leftOutputToken;
                            IRule leftRule = rules.FirstOrDefault(r => r.ID == leftIdentifier.ID);
                            left = EvaluateRule(target, leftRule);
                        }

                        if (op.Type == OperatorTypeEnum.Not) {

                            outputStack.Push(new ValueToken(!left));
                        }
                        else if (op.Type == OperatorTypeEnum.And && !left) {

                            // No need to evaluate the right value, we've already satisfied the requisite condition
                            outputStack.Pop();
                            outputStack.Push(new ValueToken(false));
                        } else if (op.Type == OperatorTypeEnum.Or && left) {

                            // No need to evaluate the right value, we've already satisfied the requisite condition
                            outputStack.Pop();
                            outputStack.Push(new ValueToken(true));
                        } else
                        {

                            Token rightOutputToken = outputStack.Pop();
                            bool right;

                            if (rightOutputToken.IsValueToken()) {

                                right = ((ValueToken) rightOutputToken).Value;
                            }
                            else {

                                IdentifierToken rightIdentifier = (IdentifierToken) rightOutputToken;
                                IRule rightRule = rules.FirstOrDefault(r => r.ID == rightIdentifier.ID);
                                right = EvaluateRule(target, rightRule);
                            }
                            
                            outputStack.Push(new ValueToken(right));
                        }
                    }
                }

                Token outcomeToken = outputStack.Pop();
                bool outcome = true;

                if (outcomeToken.IsValueToken()) {

                    outcome = ((ValueToken) outcomeToken).Value;
                }

                if (outcomeToken.IsIdentifierToken()) {

                    IRule rule = rules.FirstOrDefault(r => r.ID == ((IdentifierToken) outcomeToken).ID);
                    outcome = EvaluateRule(target, rule);
                }

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
