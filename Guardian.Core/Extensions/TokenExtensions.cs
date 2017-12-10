using System.Collections.Generic;
using System.Linq;
using Guardian.Core.Interfaces;

namespace Guardian.Core.Extensions
{
    internal static class TokenExtensions
    {
        /// <summary>
        /// Converts specified Token Stack into a string expression
        /// </summary>
        /// <param name="tokens">Token representation of a logical expression.</param>
        /// <returns></returns>
        public static string AsPostfixExpression(this Stack<IToken> tokens)
        {
            string expression = "";

            while (tokens.Any())
            {
                IToken token = tokens.Pop();

                if (token.IsOperatorToken())
                {
                    IOperator op = (IOperator) token;

                    expression += op.StringRepresentation;
                }
                else
                {
                    IIdentifier identifierToken = (IIdentifier) token;

                    expression += identifierToken.ID;
                }

                expression += " ";
            }

            return expression.Trim();
        }
    }
}