using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens {
    public class IdentifierToken : IIdentifier {
        
        public int ID { get; set; }

        public bool IsOperatorToken()
        {
            return false;
        }

        public IdentifierToken(int id) {

            ID = id;
        }
    }
}