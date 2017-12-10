using Guardian.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guardian.Library.Tokens
{
    internal class TokenValidator
    {
        private const string _validCharacters = "0123456789 |&!()";

        public void ValidateLogicalInfixExpression(string expression)
        {
            IEnumerable<char> unexpectedCharacters = expression.Where(c => !_validCharacters.Contains(c));
            if (unexpectedCharacters.Any())
            {
                // TODO: This should be logged, not exception'd
                throw new Exception(
                    $"Unable to parse expression. Found unexpected characters, '{string.Join(",", unexpectedCharacters)}'. Verify expression, '{expression}' is valid.");
            }

            if (expression.Count(c => c == '(') != expression.Count(c => c == ')'))
            {
                // TODO: This should be logged, not exception'd
                throw new Exception(
                    $"Unable to parse expression. Found a mismatch in the number of open and closing paranetheses. Verify expression, '{expression}' is valid.");
            }
        }

        public void ValidateOutputTokens(List<IToken> tokens)
        {
            if (tokens.Count < 2) return;

            for (int idx = 1; idx < tokens.Count; idx++)
            {
                IToken token = tokens[idx];
                IToken precedingToken = tokens[idx - 1];

                bool tokensAreConsecutiveOperators = token.IsOperatorToken() && precedingToken.IsOperatorToken();

                if (!tokensAreConsecutiveOperators)
                {
                    continue;
                }

                Type tokenType = token.GetType();
                Type precedingType = precedingToken.GetType();

                if (tokenType != typeof(NotOperator) && tokenType != typeof(OpenParanthesisGroupingOperator) && precedingType != typeof(CloseParanthesisGroupingOperator))
                {
                    IOperator previousOperator = (IOperator)precedingToken;
                    IOperator currentOperator = (IOperator)token;

                    throw new Exception(
                        $"Unable to parse expression. Found illegal consecutive Operators, '{previousOperator.StringRepresentation}' followed by '{currentOperator.StringRepresentation}'.");
                }
            }
        }
    }
}
