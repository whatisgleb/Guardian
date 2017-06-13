using Guardian.Library.ExpressionTree;
using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Prefix;
using Guardian.Library.Tokens;

namespace Guardian.Tests.Utilities
{
    public class TestServices {
        
        public readonly IExpressionConverter PostfixConverter;
        public readonly IExpressionConverter PrefixConverter;
        public readonly ExpressionTreeBuilder ExpressionTreeBuilder;

        public TestServices() {
            
            PostfixConverter = new Postfixer(new TokenParser());
            PrefixConverter = new Prefixer(PostfixConverter);
            ExpressionTreeBuilder = new ExpressionTreeBuilder();
        }
    }
}
