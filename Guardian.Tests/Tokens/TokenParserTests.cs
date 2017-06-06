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

                new Identifier(1)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.And),
                new Identifier(2)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.And),
                new Identifier(2),
                new Operator(OperatorTypeEnum.And),
                new Identifier(3)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(2)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(2),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(3)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.And),
                new Identifier(2),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(3)
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

                new Identifier(1),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(2),
                new Operator(OperatorTypeEnum.And),
                new Identifier(3)
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

                new Operator(OperatorTypeEnum.Not),
                new Identifier(1)
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

                new Operator(OperatorTypeEnum.OpenParanthesis),
                new Identifier(1),
                new Operator(OperatorTypeEnum.CloseParanthesis)
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

                new Operator(OperatorTypeEnum.Not),
                new Operator(OperatorTypeEnum.OpenParanthesis),
                new Identifier(1),
                new Operator(OperatorTypeEnum.Or),
                new Identifier(2),
                new Operator(OperatorTypeEnum.CloseParanthesis),
                new Operator(OperatorTypeEnum.And),
                new Identifier(3),
                new Operator(OperatorTypeEnum.Or),
                new Operator(OperatorTypeEnum.Not),
                new Identifier(4)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }
    }
}
