using Guardian.Library.ExpressionTree;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Guardian.Tests.ExpressionTrees
{
    [TestClass]
    public class ExpressionBuilderTests
    {
        private TestServices _testServices;

        public ExpressionBuilderTests()
        {
            _testServices = new TestServices();
        }

        [TestMethod]
        public void And_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.And, 1, 2);

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }

        [TestMethod]
        public void AndAnd_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2 && 3";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.And, 1,
                new ExpressionTreeNode(Operators.And, 2, 3));

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }

        [TestMethod]
        public void AndOr_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2 || 3";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or,
                new ExpressionTreeNode(Operators.And, 1, 2), 3
            );

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }

        [TestMethod]
        public void OrAnd_ExpressionTree()
        {
            // Arrange
            string expression = "1 || 2 && 3";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, 3));

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }

        [TestMethod]
        public void OrAndNot_ExpressionTree()
        {
            // Arrange
            string expression = "1 || 2 && !3";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, new ExpressionTreeNode(Operators.Not, 3)));

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }

        [TestMethod]
        public void NotOrAnd_ExpressionTree()
        {
            // Arrange
            string expression = "!(1 || 2 && 3)";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Not, new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, 3)), (ExpressionTreeNode) null);

            Assert.IsTrue(new ExpressionTreeNodeComparer().Compare(expected, root) == 0);
        }
    }
}