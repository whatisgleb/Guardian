using System.Collections.Generic;
using Guardian.Core.Extensions;
using Guardian.Core.Interfaces;
using Guardian.Core.Postfix;
using Guardian.Core.Tests.Utilities;
using Guardian.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Core.Tests.ExpressionConversions
{
    [TestClass]
    public class InfixToPostfixConversionTests
    {
        private IPostfixConverter _postFixer;

        public InfixToPostfixConversionTests()
        {
            _postFixer = new Postfixer(new TokenParser());
        }

        [TestMethod]
        public void ToPostfix_NotExpression()
        {
            // Arrange
            string expression = "!1";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 !";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndExpression()
        {
            // Arrange
            string expression = "1 && 2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndAndExpression()
        {
            // Arrange
            string expression = "1 && 2 && 3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 && &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndParanthesisAndExpression()
        {
            // Arrange
            string expression = "1 && (2 && 3)";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 && &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_ParanthesisAndAndExpression()
        {
            // Arrange
            string expression = "(1 && 2) && 3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 && 3 &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrExpression()
        {
            // Arrange
            string expression = "1 || 2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndOrExpression()
        {
            // Arrange
            string expression = "1 && 2 || 3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 && 3 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrAndExpression()
        {
            // Arrange
            string expression = "1 || 2 && 3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndNotExpression()
        {
            // Arrange
            string expression = "1 && !2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ! &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_NotAndExpression()
        {
            // Arrange
            string expression = "!1 && 2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 ! 2 &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrNotExpression()
        {
            // Arrange
            string expression = "1 || !2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ! ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_NotOrExpression()
        {
            // Arrange

            string expression = "!1 || 2";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 ! 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrAndNotExpression()
        {
            // Arrange
            string expression = "1 || 2 && !3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 ! && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndOrNotExpression()
        {
            // Arrange
            string expression = "1 && 2 || !3";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 && 3 ! ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_ParantheticalExpression()
        {
            // Arrange
            string expression = "(1 || 2)";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndParantheticalExpression()
        {
            // Arrange
            string expression = "1 && (2 || 3)";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 || &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression()
        {
            // Arrange
            string expression = "((1 || 2 && 3) || 4) || 5 && 6";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 3 && || 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression_NotInsideParanthetical()
        {
            // Arrange
            string expression = "((1 || !2 && 3) || 4) || 5 && 6";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ! 3 && || 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression_NotPrecedingParanthetical()
        {
            // Arrange
            string expression = "(!(1 || !2 && 3) || 4) || 5 && 6";

            // Act
            Stack<IToken> postfixedTokens = _postFixer.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "1 2 ! 3 && || ! 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }
    }
}