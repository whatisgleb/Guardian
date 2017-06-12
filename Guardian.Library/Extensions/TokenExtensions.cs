using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Extensions
{
    public static class TokenExtensions
    {
        /// <summary>
        /// Converts Token Stack into a Postfix string expression
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static string AsPostfixExpression(this Stack<Token> tokens)
        {
            string expression = "";

            while (tokens.Any())
            {
                Token token = tokens.Pop();

                if (token.IsOperatorToken())
                {
                    OperatorToken op = (OperatorToken)token;

                    expression += op.Mapping.StringRepresentation;
                }
                else if (token.IsIdentifierToken())
                {
                    IdentifierToken identifierToken = (IdentifierToken)token;

                    expression += identifierToken.ID;
                }

                if (tokens.Any())
                {
                    expression += " ";
                }
            }

            return expression;
        }
    }
}
