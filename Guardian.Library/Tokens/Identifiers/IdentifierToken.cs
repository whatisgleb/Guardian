namespace Guardian.Library.Tokens.Identifiers {
    public class IdentifierToken : Token {
        
        public int ID { get; set; }

        public IdentifierToken(int id) {

            ID = id;
        }
    }
}