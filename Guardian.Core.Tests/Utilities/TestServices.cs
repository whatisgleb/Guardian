using Guardian.Core.ExpressionTree;
using Guardian.Core.Interfaces;
using Guardian.Core.Postfix;
using Guardian.Core.Tokens;

namespace Guardian.Core.Tests.Utilities
{
    public class TestServices
    {
        public readonly IPostfixConverter PostfixConverter;
        public readonly ExpressionTreeBuilder ExpressionTreeBuilder;

        public TestServices()
        {
            PostfixConverter = new Postfixer(new TokenParser());
            ExpressionTreeBuilder = new ExpressionTreeBuilder();
        }
    }
}