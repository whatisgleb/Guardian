using System.Collections.Generic;

namespace Guardian.Core.Interfaces
{
    internal interface ITokenParser
    {
        List<IToken> ParseInfixExpression(string expression);
    }
}