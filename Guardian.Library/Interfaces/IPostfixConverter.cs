using System.Collections.Generic;
using Guardian.Library.Tokens;

namespace Guardian.Library.Interfaces {
    public interface IPostfixConverter {

        Stack<Token> ConvertToStack(string expression);
    }
}