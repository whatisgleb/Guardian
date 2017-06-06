using System.Collections.Generic;
using Guardian.Library.Tokens;

namespace Guardian.Library.Interfaces {
    public interface IPrefixConverter {

        Stack<Token> ConvertToStack(string expression);
    }
}