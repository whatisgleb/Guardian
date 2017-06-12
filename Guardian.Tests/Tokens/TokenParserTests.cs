using System.Collections.Generic;
using Guardian.Library.Enums;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.Tokens
{
    [TestClass]
    public class TokenParserTests
    {
        [TestMethod]
        public void Parse_Expression()
        {
            // Arrange
            string expression = "1";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_AndExpression() {

            // Arrange
            string expression = "1 && 2";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(2)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_MultipleAndExpression()
        {

            // Arrange
            string expression = "1 && 2 && 3";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(2),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_OrExpression() {

            // Arrange
            string expression = "1 || 2";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(2)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_MultipleOrExpression()
        {

            // Arrange
            string expression = "1 || 2 || 3";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(2),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_AndOrExpression()
        {

            // Arrange
            string expression = "1 && 2 || 3";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(2),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_OrAndExpression()
        {

            // Arrange
            string expression = "1 || 2 && 3";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(2),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_NotExpression() {

            // Arrange
            string expression = "!1";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new OperatorToken(OperatorTypeEnum.Not),
                new IdentifierToken(1)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_ParantheticalExpression() {

            // Arrange
            string expression = "(1)";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new OperatorToken(OperatorTypeEnum.OpenParanthesis),
                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.CloseParanthesis)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_ComplexExpression() {

            // Arrange
            string expression = "!(1 || 2) && 3 || !4";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<Token> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<Token> expectedTokens = new List<Token>() {

                new OperatorToken(OperatorTypeEnum.Not),
                new OperatorToken(OperatorTypeEnum.OpenParanthesis),
                new IdentifierToken(1),
                new OperatorToken(OperatorTypeEnum.Or),
                new IdentifierToken(2),
                new OperatorToken(OperatorTypeEnum.CloseParanthesis),
                new OperatorToken(OperatorTypeEnum.And),
                new IdentifierToken(3),
                new OperatorToken(OperatorTypeEnum.Or),
                new OperatorToken(OperatorTypeEnum.Not),
                new IdentifierToken(4)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }
    }
}
