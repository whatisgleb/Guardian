using System.Collections.Generic;
using FluentAssertions;
using Guardian.Core.ExpressionTree;
using Guardian.Core.Interfaces;
using Guardian.Core.Postfix;
using Guardian.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Core.Tests.ExpressionTrees
{
    [TestClass]
    public class ExpressionBuilderTests
    {
        private IPostfixConverter _postFixer;
        private IExpressionTreeBuilder _expressionTreeBuilder;

        public ExpressionBuilderTests()
        {
            _postFixer = new Postfixer(new TokenParser());
            _expressionTreeBuilder = new ExpressionTreeBuilder();
        }

        [TestMethod]
        public void And_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.And, 1, 2);

            root.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AndAnd_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2 && 3";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.And, 1,
                new ExpressionTreeNode(Operators.And, 2, 3));

            root.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AndOr_ExpressionTree()
        {
            // Arrange
            string expression = "1 && 2 || 3";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or,
                new ExpressionTreeNode(Operators.And, 1, 2), 3
            );

            root.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void OrAnd_ExpressionTree()
        {
            // Arrange
            string expression = "1 || 2 && 3";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, 3));

            root.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void OrAndNot_ExpressionTree()
        {
            // Arrange
            string expression = "1 || 2 && !3";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, new ExpressionTreeNode(Operators.Not, 3)));

            root.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void NotOrAnd_ExpressionTree()
        {
            // Arrange
            string expression = "!(1 || 2 && 3)";
            Stack<IToken> postfixTokens = _postFixer.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _expressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode(Operators.Not, new ExpressionTreeNode(Operators.Or, 1,
                new ExpressionTreeNode(Operators.And, 2, 3)), (ExpressionTreeNode) null);

            root.Should().BeEquivalentTo(expected);
        }
    }
}