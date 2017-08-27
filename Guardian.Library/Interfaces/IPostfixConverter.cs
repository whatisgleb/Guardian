using System.Collections.Generic;

namespace Guardian.Library.Interfaces
{
    public interface IPostfixConverter
    {
        Stack<IToken> ConvertToStack(string expression);
    }
}