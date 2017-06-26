using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens.Operators
{
    public class Operator : IToken
    {
        public bool IsOperatorToken()
        {
            return true;
        }
    }
}