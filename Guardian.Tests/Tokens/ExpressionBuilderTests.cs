using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.ExpressionTree;
using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Tokens;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.Tokens
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
        public void And_ExpressionTree() {
            
            // Arrange
            string expression = "1 && 2";
            Stack<IToken> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.And, 1, 2);

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
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

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
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

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
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

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
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

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
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

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
        }
    }
}
