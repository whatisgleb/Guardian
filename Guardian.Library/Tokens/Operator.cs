using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class Operator : IToken
    {
        public bool IsOperatorToken()
        {
            return true;
        }
    }
}