using System;
using System.Collections.Generic;
using System.Linq;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Tokens
{
    public class TokenParser : ITokenParser {

        private const string _validCharacters = "0123456789 |&!()";
        private List<IOperator> _operators = new List<IOperator>()
        {
            new AndOperator(),
            new OrOperator(),
            new NotOperator(),
            new OpenParanthesisGroupingOperator(),
            new CloseParanthesisGroupingOperator()
        }; 

        private void ValidateLogicalInfixExpression(string expression) {

            IEnumerable<char> unexpectedCharacters = expression.Where(c => !_validCharacters.Contains(c));
            if (unexpectedCharacters.Any()) {

                // TODO: This should be logged, not exception'd
                throw new Exception($"Unable to parse expression. Found unexpected characters, '{string.Join(",", unexpectedCharacters)}'. Verify expression, '{expression}' is valid.");
            }

            if (expression.Count(c => c == '(') != expression.Count(c => c == ')'))
            {
                // TODO: This should be logged, not exception'd
                throw new Exception($"Unable to parse expression. Found a mismatch in the number of open and closing paranetheses. Verify expression, '{expression}' is valid.");
            }
        }

        private void ValidateOutputTokens(List<IToken> tokens)
        {

            if (!tokens.Any()) return;

            IToken previousToken = null;

            foreach (var token in tokens)
            {
                if (previousToken == null)
                {
                    previousToken = token;
                    continue;
                }

                Type type = token.GetType();

                if (token.IsOperatorToken() && previousToken.IsOperatorToken() && type != typeof(NotOperator) && type != typeof(OpenParanthesisGroupingOperator) && previousToken.GetType() != typeof(CloseParanthesisGroupingOperator))
                {
                    IOperator previousOperator = (IOperator) previousToken;
                    IOperator currentOperator = (IOperator) token;

                    throw new Exception($"Unable to parse expression. Found illegal consecutive Operators, '{previousOperator.StringRepresentation}' followed by '{currentOperator.StringRepresentation}'.");
                }

                previousToken = token;
            }
        }

        /// <summary>
        /// Converts a string infix expression into Tokens
        /// </summary>
        /// <param name="expression">A logical infix string expression</param>
        /// <returns>A List of Tokens in the order they were encountered in the specified string expression</returns>
        public List<IToken> ParseInfixExpression(string expression) {

            ValidateLogicalInfixExpression(expression);

            // Prepare expression by first removing all spaces, then introducing spaces strategically
            // ((!(1 &&2) || 3) || 4) && !5 
            // =>
            // ((!(1&&2)||3)||4)&&!5
            // => 
            // ( ( ! ( 1 && 2 ) || 3 ) || 4 ) && ! 5
            expression = expression.Replace(" ", string.Empty);

            foreach (IOperator op in _operators)
            {
                expression = expression.Replace(op.StringRepresentation, $" {op.StringRepresentation} ");
            }

            IEnumerable<string> tokens = expression.Split(' ').Where(t => !string.IsNullOrWhiteSpace(t));
            List<IToken> outputTokens = new List<IToken>();

            foreach (string token in tokens)
            {
                IOperator op = _operators.FirstOrDefault(o => o.StringRepresentation == token);

                if (op != null)
                {
                    outputTokens.Add(op);
                }
                else
                {
                    int ID;

                    bool succeeded = int.TryParse(token, out ID);

                    if (succeeded)
                    {
                        outputTokens.Add(new IdentifierToken(ID));
                    }
                    else
                    {
                        // TODO: This should be logged, not exception'd
                        throw new Exception($"Unable to parse identifier Token. Found, '{token}'. Verify expression, '{expression}' is valid.");
                    }
                }
            }

            ValidateOutputTokens(outputTokens);

            return outputTokens;
        }
    }
}
