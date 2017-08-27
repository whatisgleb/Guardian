using System.Collections.Generic;

namespace Guardian.Library.Interfaces
{
    public interface ITokenParser
    {
        List<IToken> ParseInfixExpression(string expression);
    }
}