using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;

namespace Guardian.Library.Extensions
{
    public static class TokenExtensions
    {
        /// <summary>
        /// Converts Token Stack into a Postfix string expression
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static string AsPostfixExpression(this Stack<IToken> tokens)
        {
            string expression = "";

            while (tokens.Any())
            {
                IToken token = tokens.Pop();

                if (token.IsOperatorToken())
                {
                    IOperator op = (IOperator)token;

                    expression += op.StringRepresentation;
                }
                else
                {
                    IIdentifier identifierToken = (IIdentifier)token;

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
