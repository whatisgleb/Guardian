using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Postfix
{
    public class Postfixer : IPostfixConverter
    {
        private readonly ITokenParser _tokenParser;

        public Postfixer(ITokenParser tokenParser) {

            _tokenParser = tokenParser;
        }

        public Stack<IToken> ConvertToStack(string expression) {

            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);
            Stack<IToken> temp = new Stack<IToken>();

            // Temporary container for Operators
            Stack<IOperator> operatorStack = new Stack<IOperator>();

            foreach (var token in tokens) {

                // All Identifiers are immediately added to the output stack
                if (!token.IsOperatorToken()) {
                    
                    temp.Push(token);
                } else {

                    IOperator currentOperator = (IOperator) token;

                    if (currentOperator.GetType() == typeof(CloseParanthesisGroupingOperator)) {

                        // Encountered closing paranthesis
                        // Iterate through Operators in the OperatorToken stack until an opening paranthesis is encountered
                        // Add each OperatorToken to the output stack

                        // Begin with the first entry in the OperatorToken stack
                        currentOperator = operatorStack.Pop();

                        // Go until open paranthesis is encountered
                        while (currentOperator.GetType() != typeof(OpenParanthesisGroupingOperator)) {

                            temp.Push(currentOperator);
                            currentOperator = operatorStack.Pop();
                        }
                    }
                    else if (operatorStack.Any() && 
                        currentOperator.Precedence.HasValue &&
                        operatorStack.Peek().Precedence.HasValue &&
                        currentOperator.Precedence < operatorStack.Peek().Precedence) {

                        // Current OperatorToken has a lower precedence than the top OperatorToken in the OperatorToken stack
                        // Add OperatorToken from stack to output immediately 
                        temp.Push(operatorStack.Pop());
                        operatorStack.Push(currentOperator);
                    }
                    else {

                        // Current OperatorToken is either:
                        // - higher weight than top OperatorToken in the OperatorToken stack 
                        // - There is no OperatorToken in the OperatorToken stack 
                        // - Current OperatorToken is an opening paranthesis
                        operatorStack.Push(currentOperator);
                    }
                }
            }

            // Out of Tokens
            // Time to add anything in the OperatorToken stack to the output stack
            foreach (IOperator op in operatorStack) {
                
                temp.Push(op);
            }

            // Return Reverse Stack
            return new Stack<IToken>(temp);
        }
    }
}
