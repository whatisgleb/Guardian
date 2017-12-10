using System;
using System.Collections.Generic;
using System.Linq;
using Guardian.Core.Interfaces;

namespace Guardian.Core.Tokens
{
    public class TokenParser : ITokenParser
    {
        private TokenValidator _validator;

        public TokenParser()
        {
            _validator = new TokenValidator();
        }

        /// <summary>
        /// Converts a logical infix expression to a Postfix Token list representation.
        /// </summary>
        /// <param name="expression">A string logical infix expression.</param>
        /// <returns>Postfix Tokens list representation of an infix expression.</returns>
        public List<IToken> ParseInfixExpression(string expression)
        {
            _validator.ValidateLogicalInfixExpression(expression);

            List<IToken> outputTokens = new List<IToken>();

            // Prepare expression for efficient parsing:
            // - Remove all spaces
            // - Introduce spaces before and after each operator
            // - Split on spaces removing any empty items
            // - Parse
            // -- Ex: (1 && !2) -> (1&&!2) -> ( 1 && ! 2 ) -> [(, 1, &&, !, 2, )]
            expression = expression.Replace(" ", string.Empty);

            foreach (IOperator op in Operators.All.Values)
            {
                expression = expression.Replace(op.StringRepresentation, $" {op.StringRepresentation} ");
            }

            IEnumerable<string> tokens = expression.Split(' ').Where(t => !string.IsNullOrWhiteSpace(t));

            foreach (string token in tokens)
            {
                if (Operators.All.ContainsKey(token))
                {
                    // Token is an operator
                    outputTokens.Add(Operators.All[token]);
                }
                else
                {
                    // Token is an identifier
                    int ID;

                    bool succeeded = int.TryParse(token, out ID);

                    if (succeeded)
                    {
                        outputTokens.Add(new IdentifierToken(ID));
                    }
                    else
                    {
                        // TODO: This should be logged, not exception'd
                        throw new Exception(
                            $"Unable to parse identifier Token. Found, '{token}'. Verify expression, '{expression}' is valid.");
                    }
                }
            }

            _validator.ValidateOutputTokens(outputTokens);

            return outputTokens;
        }
    }
}