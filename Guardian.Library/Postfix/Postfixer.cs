using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Postfix
{
    public class Postfixer : IExpressionConverter
    {
        private readonly ITokenParser _tokenParser;

        public Postfixer(ITokenParser tokenParser) {

            _tokenParser = tokenParser;
        }

        public Stack<Token> ConvertToStack(string expression) {

            List<Token> tokens = _tokenParser.ParseInfixExpression(expression);
            Stack<Token> temp = new Stack<Token>();

            // Temporary container for Operators
            Stack<OperatorToken> operatorStack = new Stack<OperatorToken>();

            foreach (var token in tokens) {

                Type tokenType = token.GetType();

                // All Identifiers are immediately added to the output stack
                if (tokenType == typeof(IdentifierToken)) {
                    
                    temp.Push(token);
                }

                if (tokenType == typeof(OperatorToken)) {

                    OperatorToken currentOperatorToken = (OperatorToken) token;

                    if (currentOperatorToken.Type == OperatorTypeEnum.CloseParanthesis) {

                        // Encountered closing paranthesis
                        // Iterate through Operators in the OperatorToken stack until an opening paranthesis is encountered
                        // Add each OperatorToken to the output stack

                        // Begin with the first entry in the OperatorToken stack
                        currentOperatorToken = operatorStack.Pop();

                        // Go until open paranthesis is encountered
                        while (currentOperatorToken.Type != OperatorTypeEnum.OpenParanthesis) {

                            temp.Push(currentOperatorToken);
                            currentOperatorToken = operatorStack.Pop();
                        }
                    }
                    else if (Operators.Weights.ContainsKey(currentOperatorToken.Type) &&
                        operatorStack.Any() &&
                        Operators.Weights.ContainsKey(operatorStack.Peek().Type) &&
                        Operators.Weights[currentOperatorToken.Type] < Operators.Weights[operatorStack.Peek().Type]) {

                        // Current OperatorToken has a lower precedence than the top OperatorToken in the OperatorToken stack
                        // Add OperatorToken from stack to output immediately 
                        temp.Push(operatorStack.Pop());
                        operatorStack.Push(currentOperatorToken);
                    }
                    else {

                        // Current OperatorToken is either:
                        // - higher weight than top OperatorToken in the OperatorToken stack 
                        // - There is no OperatorToken in the OperatorToken stack 
                        // - Current OperatorToken is an opening paranthesis
                        operatorStack.Push(currentOperatorToken);
                    }
                }
            }

            // Out of Tokens
            // Time to add anything in the OperatorToken stack to the output stack
            foreach (OperatorToken op in operatorStack) {
                
                temp.Push(op);
            }

            // Reverse Stack
            Stack<Token> output = new Stack<Token>();

            while (temp.Any()) {
                
                output.Push(temp.Pop());
            }

            return output;
        }
    }
}
