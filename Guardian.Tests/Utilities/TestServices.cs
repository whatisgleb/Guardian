using Guardian.Library.ExpressionTree;
using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Tokens;

namespace Guardian.Tests.Utilities
{
    public class TestServices {
        
        public readonly IPostfixConverter PostfixConverter;
        public readonly ExpressionTreeBuilder ExpressionTreeBuilder;

        public TestServices() {
            
            PostfixConverter = new Postfixer(new TokenParser());
            ExpressionTreeBuilder = new ExpressionTreeBuilder();
        }
    }
}
