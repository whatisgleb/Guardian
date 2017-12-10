using System.Collections.Generic;

namespace Guardian.Core.Interfaces
{
    internal interface IPostfixConverter
    {
        Stack<IToken> ConvertToStack(string expression);
    }
}