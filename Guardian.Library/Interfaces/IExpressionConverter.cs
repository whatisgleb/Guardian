using System.Collections.Generic;
using Guardian.Library.Tokens;

namespace Guardian.Library.Interfaces {
    public interface IExpressionConverter {

        Stack<Token> ConvertToStack(string expression);
    }
}