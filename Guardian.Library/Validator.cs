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
        private IPostfixConverter _postfixConverter;

        public Validator(IPostfixConverter psotfixConverter) {
            
            _postfixConverter = psotfixConverter;
        }

        public List<ValidationError> Validate(object target, IEnumerable<IRuleGroup> ruleGroups, IEnumerable<IRule> rules) {

            List<ValidationError> errors = new List<ValidationError>();
            Dictionary<int, bool> results = new Dictionary<int, bool>();

            // Evaluate rule groups
            foreach (var ruleGroup in ruleGroups) {
                
                Stack<bool> values = new Stack<bool>();
                Stack<Token> tokenStack = _postfixConverter.ConvertToStack(ruleGroup.Expression);

                // Evaluate necessary rules
                List<int> missingRuleIDs =
                    tokenStack.Where(t => t.IsIdentifier())
                        .Select(t => ((Identifier) t).ID)
                        .Where(i => !results.ContainsKey(i))
                        .ToList();

                foreach (IRule rule in rules.Where(r => missingRuleIDs.Contains(r.ID))) {

                    results.Add(rule.ID, EvaluateRule(target, rule));
                }

                // Iterate through stack
                while (tokenStack.Any()) {

                    Token token = tokenStack.Pop();

                    if (token.IsIdentifier()) {

                        Identifier identifier = (Identifier) token;

                        values.Push(results[identifier.ID]);
                    }

                    if (token.IsOperator()) {

                        Operator op = (Operator) token;

                        bool val1 = values.Pop();

                        if (op.Type == OperatorTypeEnum.And) {

                            bool val2 = values.Pop();
                            values.Push(val1 && val2);
                        }

                        if (op.Type == OperatorTypeEnum.Or) {

                            bool val2 = values.Pop();
                            values.Push(val1 || val2);
                        }

                        if (op.Type == OperatorTypeEnum.Not) {
                            
                            values.Push(!val1);
                        }
                    }
                }

                bool finalValue = values.Pop();

                if (values.Any()) {
                    
                    throw new Exception("An unexpected number of outcomes was found after evluating a Rule Group.");
                }

                if (finalValue) {
                    
                    errors.Add(new ValidationError() {
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
