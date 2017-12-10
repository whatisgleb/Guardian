using System.Collections.Generic;

namespace Guardian.Core.Interfaces
{
    public interface ITokenParser
    {
        List<IToken> ParseInfixExpression(string expression);
    }
}