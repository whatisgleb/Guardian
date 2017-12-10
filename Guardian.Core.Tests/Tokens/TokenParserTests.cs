using System.Collections.Generic;
using Guardian.Core.Interfaces;
using Guardian.Core.Tests.Utilities;
using Guardian.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Core.Tests.Tokens
{
    [TestClass]
    public class TokenParserTests
    {
        private TokenParser _tokenParser;
        private TokenComparer _tokenComparer;

        [TestInitialize]
        public void TestInitialize()
        {
            _tokenParser = new TokenParser();;
            _tokenComparer = new TokenComparer();
        }

        [TestMethod]
        public void Parse_Expression()
        {
            // Arrange
            string expression = "1";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_AndExpression()
        {
            // Arrange
            string expression = "1 && 2";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.And,
                new IdentifierToken(2)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_MultipleAndExpression()
        {
            // Arrange
            string expression = "1 && 2 && 3";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.And,
                new IdentifierToken(2),
                Operators.And,
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_OrExpression()
        {
            // Arrange
            string expression = "1 || 2";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.Or,
                new IdentifierToken(2)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_MultipleOrExpression()
        {
            // Arrange
            string expression = "1 || 2 || 3";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.Or,
                new IdentifierToken(2),
                Operators.Or,
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_AndOrExpression()
        {
            // Arrange
            string expression = "1 && 2 || 3";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.And,
                new IdentifierToken(2),
                Operators.Or,
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_OrAndExpression()
        {
            // Arrange
            string expression = "1 || 2 && 3";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                new IdentifierToken(1),
                Operators.Or,
                new IdentifierToken(2),
                Operators.And,
                new IdentifierToken(3)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_NotExpression()
        {
            // Arrange
            string expression = "!1";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                Operators.Not,
                new IdentifierToken(1)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_ParantheticalExpression()
        {
            // Arrange
            string expression = "(1)";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                Operators.OpenParanthesis,
                new IdentifierToken(1),
                Operators.CloseParanthesis
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }

        [TestMethod]
        public void Parse_ComplexExpression()
        {
            // Arrange
            string expression = "!(1 || 2) && 3 || !4";

            // Act
            List<IToken> tokens = _tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>()
            {
                Operators.Not,
                Operators.OpenParanthesis,
                new IdentifierToken(1),
                Operators.Or,
                new IdentifierToken(2),
                Operators.CloseParanthesis,
                Operators.And,
                new IdentifierToken(3),
                Operators.Or,
                Operators.Not,
                new IdentifierToken(4)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, _tokenComparer);
        }
    }
}