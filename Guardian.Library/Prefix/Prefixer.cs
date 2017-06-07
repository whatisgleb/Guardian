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

namespace Guardian.Library.Prefix
{
    public class Prefixer : IPrefixConverter
    {
        private readonly IPostfixConverter _postfixConverter;

        public Prefixer(IPostfixConverter postfixConverter) {

            _postfixConverter = postfixConverter;
        }

        public Stack<Token> ConvertToStack(string expression) {

            Stack<Token> postfixStack = _postfixConverter.ConvertToStack(expression);

            Stack<List<Token>> tempStack = new Stack<List<Token>>();

            // TODO: Add comments
            foreach (Token token in postfixStack)
            {

                Type tokenType = token.GetType();

                if (tokenType == typeof(Identifier))
                {

                    tempStack.Push(new List<Token>() { token });
                }

                if (tokenType == typeof(Operator))
                {

                    Operator op = (Operator)token;
                    List<Token> currentTopTokens = tempStack.Pop();

                    if (op.Type == OperatorTypeEnum.Not)
                    {

                        currentTopTokens.Insert(0, token);
                        tempStack.Push(currentTopTokens);
                    }
                    else
                    {

                        List<Token> currentSecondFromTopTokens = tempStack.Pop();
                        currentSecondFromTopTokens.AddRange(currentTopTokens);
                        currentSecondFromTopTokens.Insert(0, token);
                        tempStack.Push(currentSecondFromTopTokens);
                    }
                }
            }

            List<Token> tokens = tempStack.Pop();
            tokens.Reverse();
            Stack<Token> output = new Stack<Token>(tokens);

            if (tempStack.Any())
            {
                throw new Exception($"Unable to parse expression. Found an unexpected mismatch in the number of operands to operators.");
            }

            return output;
        }
    }
}
