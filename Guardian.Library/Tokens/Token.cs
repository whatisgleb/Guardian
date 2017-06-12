using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;
using Guardian.Library.Tokens.Values;

namespace Guardian.Library.Tokens
{
    public class Token
    {
        public bool IsOperatorToken() {

            return this.GetType() == typeof(OperatorToken);
        }

        public bool IsIdentifierToken() {

            return this.GetType() == typeof(IdentifierToken);
        }

        public bool IsValueToken() {

            return this.GetType() == typeof(ValueToken);
        }
    }
}
