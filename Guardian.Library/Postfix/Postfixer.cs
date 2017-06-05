using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Postfix
{
    public class Postfixer
    {
        public Stack<Token> ConvertToPostfixStack(IEnumerable<Token> tokens) {

            Stack<Token> temp = new Stack<Token>();

            // Temporary container for Operators
            Stack<Operator> operatorStack = new Stack<Operator>();

            foreach (var token in tokens) {

                Type tokenType = token.GetType();

                // All Identifiers are immediately added to the output stack
                if (tokenType == typeof(Identifier)) {
                    
                    temp.Push(token);
                }

                if (tokenType == typeof(Operator)) {

                    Operator currentOperator = (Operator) token;

                    if (currentOperator.Type == OperatorTypeEnum.CloseParanthesis) {

                        // Encountered closing paranthesis
                        // Iterate through Operators in the Operator stack until an opening paranthesis is encountered
                        // Add each Operator to the output stack

                        // Begin with the first entry in the Operator stack
                        currentOperator = operatorStack.Pop();

                        // Go until open paranthesis is encountered
                        while (currentOperator.Type != OperatorTypeEnum.OpenParanthesis) {

                            temp.Push(currentOperator);
                            currentOperator = operatorStack.Pop();
                        }
                    }
                    else if (Operators.Weights.ContainsKey(currentOperator.Type) &&
                        operatorStack.Any() &&
                        Operators.Weights.ContainsKey(operatorStack.Peek().Type) &&
                        Operators.Weights[currentOperator.Type] < Operators.Weights[operatorStack.Peek().Type]) {

                        // Current Operator has a lower precedence than the top Operator in the Operator stack
                        // Add Operator from stack to output immediately 
                        temp.Push(operatorStack.Pop());
                        operatorStack.Push(currentOperator);
                    }
                    else {

                        // Current Operator is either:
                        // - higher weight than top Operator in the Operator stack 
                        // - There is no Operator in the Operator stack 
                        // - Current Operator is an opening paranthesis
                        operatorStack.Push(currentOperator);
                    }
                }
            }

            // Out of Tokens
            // Time to add anything in the Operator stack to the output stack
            foreach (Operator op in operatorStack) {
                
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
