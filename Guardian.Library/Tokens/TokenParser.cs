using System;
using System.Collections.Generic;
using System.Linq;
using Guardian.Library.Enums;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Tokens
{
    public class TokenParser {

        private const string _validCharacters = "0123456789 |&!()";

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

        private void ValidateEncounteredOperatorMakesSense(OperatorMapping currentMapping, List<Token> tokens) {

            if (!tokens.Any()) return;

            Token previousToken = tokens.Last();

            if (previousToken == null || !previousToken.IsOperator()) return;

            Operator previousOp = (Operator) previousToken;

            if (currentMapping.Type != OperatorTypeEnum.Not && currentMapping.Type != OperatorTypeEnum.OpenParanthesis && previousOp.Mapping.Type != OperatorTypeEnum.CloseParanthesis) {

                // TODO: This should be logged, not exception'd
                throw new Exception($"Unable to parse expression. Found illegal consecutive Operators, '{previousOp.Mapping.StringRepresentation}' followed by '{currentMapping.StringRepresentation}'.");
            }
        }

        /// <summary>
        /// Converts a string infix expression into Tokens
        /// </summary>
        /// <param name="expression">A logical infix string expression</param>
        /// <returns>A List of Tokens in the order they were encountered in the specified string expression</returns>
        public List<Token> ParseInfixExpression(string expression) {

            ValidateLogicalInfixExpression(expression);

            // Strategy is to go through each character in the expression one at a time
            // Understand if the character is an Operator character or an Identifier character
            // Apply rules and build Tokens
            string currentToken = "";
            List<Token> outputTokens = new List<Token>();

            for (int index = 0; index < expression.Length; index++) {

                char character = expression[index];
                bool isLastCharacter = index == expression.Length - 1;
                bool isNextCharacterAnOperatorCharacter = !isLastCharacter && Operators.Operators.Mapping.Any(c => c.Character == expression[index + 1]);

                if (Operators.Operators.Mapping.Any(c => c.Character == character)) {

                    // This is an Operator character
                    OperatorMapping operatorMapping = Operators.Operators.Mapping.FirstOrDefault(c => c.Character == character);

                    ValidateEncounteredOperatorMakesSense(operatorMapping, outputTokens);

                    // Add onto current token
                    currentToken += character;

                    // If token length matches requirement, add token
                    if (currentToken.Length == operatorMapping.RequiredConsecutiveCharacterCount) {

                        if (currentToken.All(c => c == operatorMapping.Character)) {

                            outputTokens.Add(new Operator(operatorMapping.Type));
                            currentToken = "";
                        }
                        else {

                            // Something's wrong, unexpected token composition
                            // TODO: This should be logged, not exception'd
                            throw new Exception($"Unable to parse Token. Found, '{currentToken}'. Verify expression, '{expression}' is valid.");
                        }
                    }
                }
                else {

                    // This is an Identifier character
                    if (character != ' ' || isLastCharacter) {

                        currentToken += character;
                    }

                    if (!string.IsNullOrWhiteSpace(currentToken) && (character == ' ' || isLastCharacter || isNextCharacterAnOperatorCharacter)) {
                        
                        // End of token
                        int identifier;
                        bool succeeded = int.TryParse(currentToken, out identifier);

                        if (succeeded) {

                            outputTokens.Add(new Identifier(identifier));
                        }
                        else {

                            // TODO: This should be logged, not exception'd
                            throw new Exception($"Unable to parse identifier Token. Found, '{currentToken}'. Verify expression, '{expression}' is valid.");
                        }

                        currentToken = "";
                    }
                }
            }

            return outputTokens;
        }

    }
}
