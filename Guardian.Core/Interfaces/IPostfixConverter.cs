using System.Collections.Generic;

namespace Guardian.Core.Interfaces
{
    public interface IPostfixConverter
    {
        Stack<IToken> ConvertToStack(string expression);
    }
}