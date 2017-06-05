using Guardian.Library.Postfix;
using Guardian.Library.Tokens;

namespace Guardian.Tests.Utilities
{
    public class TestServices {

        public readonly TokenParser TokenParser;
        public readonly Postfixer Postfixer;

        public TestServices() {
            
            Postfixer = new Postfixer();
            TokenParser = new TokenParser();
        }
    }
}
