using System;
using Guardian.Library.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.Tokens
{
    [TestClass]
    public class TokenParserValidationTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Consecutive_AndOperators()
        {
            // Arrange
            string expression = "1 && && 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Consecutive_AndClosingParanthesisOperators()
        {
            // Arrange
            string expression = "(1 &&) 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_IsMissing_OpeningParanthesis()
        {
            // Arrange
            string expression = "1 && 2)";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_IsMissing_ClosingParanthesis()
        {
            // Arrange
            string expression = "(1 && 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_IncompleteOperatorToken()
        {

            // Arrange
            string expression = "1 & 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_InvalidOperatorToken()
        {

            // Arrange
            string expression = "1 &| 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_AlphaCharacter()
        {
            // Arrange
            string expression = "1 a 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_AdditionOperatorToken()
        {
            // Arrange
            string expression = "1 + 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_DivisionOperatorToken()
        {
            // Arrange
            string expression = "1 / 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_SubtractionOperatorToken()
        {
            // Arrange
            string expression = "1 - 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_MultiplicationOperatorToken()
        {
            // Arrange
            string expression = "1 * 2";

            // Act
            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_ModulusOperatorToken()
        {
            // Arrange
            string expression = "1 % 2";

            // Act
            TokenParser tokenParser = new TokenParser();
            
            tokenParser.ParseInfixExpression(expression);
        }
    }
}
