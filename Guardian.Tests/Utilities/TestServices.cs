using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Prefix;
using Guardian.Library.Tokens;

namespace Guardian.Tests.Utilities
{
    public class TestServices {
        
        public readonly IPostfixConverter PostfixConverter;
        public readonly IPrefixConverter PrefixConverter;

        public TestServices() {
            
            PostfixConverter = new Postfixer(new TokenParser());
            PrefixConverter = new Prefixer(PostfixConverter);
        }
    }
}
