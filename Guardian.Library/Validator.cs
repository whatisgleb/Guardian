using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.ExpressionTree;
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
        private ExpressionTreeBuilder _treeBuilder;
        private Dictionary<int, bool> _ruleOutcomes;

        public Validator(IPostfixConverter postfixConverter) {
            
            _postfixConverter = postfixConverter;
            _treeBuilder = new ExpressionTreeBuilder();
            _ruleOutcomes = new Dictionary<int, bool>();
        }

        public List<ValidationError> Validate(object target, IEnumerable<IRuleGroup> ruleGroups, IEnumerable<IRule> rules) {

            List<ValidationError> errors = new List<ValidationError>();

            // Evaluate rule groups
            foreach (var ruleGroup in ruleGroups) {

                Stack<IToken> postfixTokens = _postfixConverter.ConvertToStack(ruleGroup.Expression);
                ExpressionTreeNode root = _treeBuilder.BuildExpressionTree(postfixTokens);

                if (EvaluateNode(root, target, rules))
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

        private bool EvaluateNode(ExpressionTreeNode node, object target, IEnumerable<IRule> rules)
        {
            if (!node.Token.IsOperatorToken()) {

                IIdentifier identifier = (IIdentifier) node.Token;

                IRule rule = rules.FirstOrDefault(r => r.ID == identifier.ID);

                return EvaluateRule(target, rule);
            }
            else {

                IOperator op = (IOperator) node.Token;

                return op.Evaluate(() => EvaluateNode(node.Left, target, rules), () => EvaluateNode(node.Right, target, rules));
            }
        }

        private bool EvaluateRule(object target, IRule rule) {

            if (!_ruleOutcomes.ContainsKey(rule.ID)) {

                var parameter = Expression.Parameter(target.GetType(), target.GetType().Name);
                var expression = DynamicExpression.ParseLambda(new[] {parameter}, typeof(bool), rule.Expression);

                bool outcome = (bool) expression.Compile().DynamicInvoke(target);

                _ruleOutcomes.Add(rule.ID, outcome);
            }

            return _ruleOutcomes[rule.ID];
        }
    }
}

