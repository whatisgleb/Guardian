using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guardian.Interfaces;

namespace Guardian
{
    public class OperatorConfiguration {

        public OperatorConfiguration(OperatorTypeEnum type, int requiredConsecutiveCharacterCount) {

            Type = type;
            RequiredConsecutiveCharacterCount = requiredConsecutiveCharacterCount;
        }

        public int RequiredConsecutiveCharacterCount { get; set; }
        public OperatorTypeEnum Type { get; set; }
    }

    public class Tokenizer {

        private readonly Dictionary<char, OperatorConfiguration> _configuration = new Dictionary<char, OperatorConfiguration>() {
            { '&', new OperatorConfiguration(OperatorTypeEnum.And, 2) },
            { '|', new OperatorConfiguration(OperatorTypeEnum.Or, 2) },
            { '!', new OperatorConfiguration(OperatorTypeEnum.Not, 1) },
            { '(', new OperatorConfiguration(OperatorTypeEnum.OpenParanthesis, 1) },
            { ')', new OperatorConfiguration(OperatorTypeEnum.CloseParanthesis, 1) },
        };

        public List<Token> GetTokens(string infixExpression) {

            List<Token> tokens = new List<Token>();
            string currentTokenString = "";

            foreach (var character in infixExpression) {

                if (_configuration.ContainsKey(character)) {

                    // Operator
                    OperatorConfiguration operatorConfiguration = _configuration[character];

                    // Add onto current token
                    currentTokenString += character;

                    // If token length matches requirement, add token
                    if (currentTokenString.Length == operatorConfiguration.RequiredConsecutiveCharacterCount) {

                        if (currentTokenString.All(c => c == character)) {

                            tokens.Add(new Operator(operatorConfiguration.Type));
                            currentTokenString = "";
                        }
                        else {

                            // Something's wrong, unexpected token composition
                            // TODO: This should be logged, not exception'd
                            throw new Exception(
                                $"Unable to parse identifier token. Found, '{currentTokenString}' while attempting to Tokenize infix expression, '{infixExpression}'. Verify expression is valid.");
                        }
                    }
                }
                else {

                    // Identifier
                    if (character != ' ') {

                        currentTokenString += character;
                    }
                    else {
                        
                        // End of token
                        int identifier;
                        bool succeeded = int.TryParse(currentTokenString, out identifier);

                        if (!succeeded) {

                            // TODO: This should be logged, not exception'd
                            throw new Exception(
                                $"Unable to parse identifier token. Found, '{currentTokenString}' while attempting to Tokenize infix expression, '{infixExpression}'. Verify expression is valid.");
                        }
                        else {
                            
                            tokens.Add(new Identifier(identifier));
                        }

                        currentTokenString = "";
                    }
                }
            }

            return tokens;
        }
    }
}
