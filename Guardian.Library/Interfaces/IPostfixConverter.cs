using System.Collections.Generic;
using Guardian.Library.Tokens;

namespace Guardian.Library.Interfaces
{
    public interface IPostfixConverter
    {
        Stack<IToken> ConvertToStack(string expression);
    }
}