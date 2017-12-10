using Guardian.Core.Interfaces;

namespace Guardian.Core.Tokens
{
    internal class IdentifierToken : IIdentifier
    {
        public int ID { get; set; }

        public bool IsOperatorToken()
        {
            return false;
        }

        public IdentifierToken(int id)
        {
            ID = id;
        }
    }
}