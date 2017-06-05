using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Tokens
{
    public class Token
    {
        public bool IsOperator() {

            return this.GetType() == typeof(Operator);
        }

        public bool IsIdentifier() {

            return this.GetType() == typeof(Identifier);
        }
    }
}
