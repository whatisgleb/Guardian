using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Guardian.Common.Interfaces;
using Guardian.Core.ExpressionTree;
using Guardian.Core.Interfaces;
using Guardian.Core.Postfix;
using Guardian.Core.Tokens;
using DynamicExpression = System.Linq.Dynamic.DynamicExpression;

namespace Guardian.Core
{
    public class Validator
    {
        private IPostfixConverter _postfixConverter;
        private ExpressionTreeBuilder _treeBuilder;
        private Dictionary<int, bool> _validationConditionResultDictionary;

        public Validator()
        {
            _postfixConverter = new Postfixer(new TokenParser());
            _treeBuilder = new ExpressionTreeBuilder();
            _validationConditionResultDictionary = new Dictionary<int, bool>();
        }

        public List<ValidationError> Validate<T>(T target, IEnumerable<IValidation> validations, IEnumerable<IValidationCondition> validationConditions)
        {
            List<ValidationError> errors = new List<ValidationError>();

            // Evaluate validation conditions
            foreach (var validation in validations)
            {
                Stack<IToken> postfixTokens = _postfixConverter.ConvertToStack(validation.Expression);
                ExpressionTreeNode root = _treeBuilder.BuildExpressionTree(postfixTokens);

                if (EvaluateNode(root, target, validationConditions))
                {
                    errors.Add(new ValidationError()
                    {
                        ErrorMessage = validation.ErrorMessage,
                        ErrorCode = validation.ErrorCode
                    });
                }
            }

            return errors;
        }

        private bool EvaluateNode<T>(ExpressionTreeNode node, T target, IEnumerable<IValidationCondition> validationConditions)
        {
            if (!node.Token.IsOperatorToken())
            {
                IIdentifier identifier = (IIdentifier) node.Token;

                IValidationCondition validationCondition = validationConditions.FirstOrDefault(r => r.ValidationConditionID == identifier.ID);

                return EvaluateValidationCondition(target, validationCondition);
            }
            else
            {
                IOperator op = (IOperator) node.Token;

                return op.Evaluate(() => EvaluateNode(node.Left, target, validationConditions),
                    () => EvaluateNode(node.Right, target, validationConditions));
            }
        }

        private bool EvaluateValidationCondition<T>(T target, IValidationCondition validationCondition)
        {
            if (!_validationConditionResultDictionary.ContainsKey(validationCondition.ValidationConditionID))
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(T), typeof(T).Name);
                LambdaExpression expression = DynamicExpression.ParseLambda(new[] {parameterExpression}, typeof(bool), validationCondition.Expression);

                bool outcome = (bool) expression.Compile().DynamicInvoke(target);

                _validationConditionResultDictionary.Add(validationCondition.ValidationConditionID, outcome);
            }

            return _validationConditionResultDictionary[validationCondition.ValidationConditionID];
        }
    }
}