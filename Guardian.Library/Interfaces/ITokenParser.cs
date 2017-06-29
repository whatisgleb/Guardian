using System.Collections.Generic;
using Guardian.Library.Tokens;

namespace Guardian.Library.Interfaces
{
    public interface ITokenParser
    {
        List<IToken> ParseInfixExpression(string expression);
    }
}