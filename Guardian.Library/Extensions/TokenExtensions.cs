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

                if (token.IsOperator())
                {
                    Operator op = (Operator)token;

                    expression += op.Mapping.StringRepresentation;
                }
                else if (token.IsIdentifier())
                {
                    Identifier identifier = (Identifier)token;

                    expression += identifier.ID;
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
