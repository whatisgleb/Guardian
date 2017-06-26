using System.Collections.Generic;
using Guardian.Library.Interfaces;
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new AndOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new AndOperator(),
                new IdentifierToken(2),
                new AndOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new OrOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new OrOperator(),
                new IdentifierToken(2),
                new OrOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new AndOperator(),
                new IdentifierToken(2),
                new OrOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new IdentifierToken(1),
                new OrOperator(),
                new IdentifierToken(2),
                new AndOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new NotOperator(),
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
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new OpenParanthesisGroupingOperator(),
                new IdentifierToken(1),
                new CloseParanthesisGroupingOperator()
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }

        [TestMethod]
        public void Parse_ComplexExpression() {

            // Arrange
            string expression = "!(1 || 2) && 3 || !4";

            TokenParser tokenParser = new TokenParser();

            // Act
            List<IToken> tokens = tokenParser.ParseInfixExpression(expression);

            // Assert
            List<IToken> expectedTokens = new List<IToken>() {

                new NotOperator(),
                new OpenParanthesisGroupingOperator(),
                new IdentifierToken(1),
                new OrOperator(),
                new IdentifierToken(2),
                new CloseParanthesisGroupingOperator(),
                new AndOperator(),
                new IdentifierToken(3),
                new OrOperator(),
                new NotOperator(),
                new IdentifierToken(4)
            };

            CollectionAssert.AreEqual(expectedTokens, tokens, new TokenComparer());
        }
    }
}
