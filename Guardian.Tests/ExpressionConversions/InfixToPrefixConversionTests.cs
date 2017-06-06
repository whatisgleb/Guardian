using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Extensions;
using Guardian.Library.Tokens;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.ExpressionConversions
{
    [TestClass]
    public class InfixToPrefixConversionTests
    {
        private readonly TestServices _testServices;

        public InfixToPrefixConversionTests()
        {

            _testServices = new TestServices();
        }

        [TestMethod]
        public void ToPrefix_NotExpression()
        {
            // Arrange
            string expression = "!1";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "! 1";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_AndExpression()
        {
            // Arrange
            string expression = "1 && 2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "&& 1 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_OrExpression()
        {
            // Arrange
            string expression = "1 || 2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| 1 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_AndOrExpression()
        {
            // Arrange
            string expression = "1 && 2 || 3";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| && 1 2 3";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_OrAndExpression()
        {
            // Arrange
            string expression = "1 || 2 && 3";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| 1 && 2 3";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_AndNotExpression()
        {
            // Arrange

            string expression = "1 && !2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "&& 1 ! 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_NotAndExpression()
        {
            // Arrange

            string expression = "!1 && 2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "&& ! 1 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_OrNotExpression()
        {
            // Arrange

            string expression = "1 || !2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| 1 ! 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_NotOrExpression()
        {
            // Arrange

            string expression = "!1 || 2";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| ! 1 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_OrAndNotExpression()
        {
            // Arrange

            string expression = "1 || 2 && !3";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| 1 && 2 ! 3";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_AndOrNotExpression()
        {
            // Arrange

            string expression = "1 && 2 || !3";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| && 1 2 ! 3";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_ParantheticalExpression()
        {
            // Arrange

            string expression = "(1 || 2)";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| 1 2";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_AndParantheticalExpression()
        {
            // Arrange
            string expression = "1 && (2 || 3)";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "&& 1 || 2 3";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPrefix_ComplexParantheticalExpression()
        {
            // Arrange
            string expression = "((1 || 2 && 3) || 4) || 5 && 6";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| || || 1 && 2 3 4 && 5 6";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPrefix_ComplexParantheticalExpression_NotInsideParanthetical()
        {
            // Arrange
            string expression = "((1 || !2 && 3) || 4) || 5 && 6";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| || || 1 && ! 2 3 4 && 5 6";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPrefix_ComplexParantheticalExpression_NotPrecedingParanthetical()
        {
            // Arrange
            string expression = "(!(1 || !2 && 3) || 4) || 5 && 6";

            // Act
            Stack<Token> postfixedTokens = _testServices.PrefixConverter.ConvertToStack(expression);

            // Assert
            string expectedPostfixExpression = "|| || || 1 && ! 2 3 4 && 5 6";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }
    }
}
