using System;
using FluentAssertions;
using Guardian.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Core.Tests.Tokens
{
    [TestClass]
    public class TokenParserValidationTests
    {
        private TokenParser _parser;

        [TestInitialize]
        public void TestInitialize()
        {
            _parser = new TokenParser();
        }

        [TestMethod]
        public void Expression_Consecutive_AndOperators()
        {
            // Arrange
            string expression = "1 && && 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Consecutive_AndClosingParanthesisOperators()
        {
            // Arrange
            string expression = "(1 &&) 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_IsMissing_OpeningParanthesis()
        {
            // Arrange
            string expression = "1 && 2)";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_IsMissing_ClosingParanthesis()
        {
            // Arrange
            string expression = "(1 && 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_IncompleteOperatorToken()
        {
            // Arrange
            string expression = "1 & 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_InvalidOperatorToken()
        {
            // Arrange
            string expression = "1 &| 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_AlphaCharacter()
        {
            // Arrange
            string expression = "1 a 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_AdditionOperatorToken()
        {
            // Arrange
            string expression = "1 + 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_DivisionOperatorToken()
        {
            // Arrange
            string expression = "1 / 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_SubtractionOperatorToken()
        {
            // Arrange
            string expression = "1 - 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_MultiplicationOperatorToken()
        {
            // Arrange
            string expression = "1 * 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Expression_Contains_ModulusOperatorToken()
        {
            // Arrange
            string expression = "1 % 2";

            // Act
            Action action = () => _parser.ParseInfixExpression(expression);

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}