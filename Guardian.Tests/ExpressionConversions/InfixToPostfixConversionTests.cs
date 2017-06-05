using System.Collections.Generic;
using Guardian.Library.Extensions;
using Guardian.Library.Tokens;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.ExpressionConversions
{
    [TestClass]
    public class InfixToPostfixConversionTests {

        private readonly TestServices _testServices;

        public InfixToPostfixConversionTests() {
        
            _testServices = new TestServices();
        }

        [TestMethod]
        public void ToPostfix_NotExpression()
        {

            string expression = "!1";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 !";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndExpression() {

            string expression = "1 && 2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrExpression()
        {
            string expression = "1 || 2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndOrExpression()
        {
            string expression = "1 && 2 || 3";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 && 3 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrAndExpression()
        {
            string expression = "1 || 2 && 3";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 3 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndNotExpression() {

            string expression = "1 && !2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ! &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_NotAndExpression()
        {

            string expression = "!1 && 2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 ! 2 &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrNotExpression()
        {

            string expression = "1 || !2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ! ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_NotOrExpression()
        {

            string expression = "!1 || 2";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 ! 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_OrAndNotExpression() {

            string expression = "1 || 2 && !3";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 3 ! && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndOrNotExpression()
        {

            string expression = "1 && 2 || !3";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 && 3 ! ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_ParantheticalExpression() {

            string expression = "(1 || 2)";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_AndParantheticalExpression() {

            string expression = "1 && (2 || 3)";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 3 || &&";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression()
        {
            string expression = "((1 || 2 && 3) || 4) || 5 && 6";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 3 && || 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }

        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression_NotInsideParanthetical()
        {
            string expression = "((1 || !2 && 3) || 4) || 5 && 6";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ! 3 && || 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }


        [TestMethod]
        public void ToPostfix_ComplexParantheticalExpression_NotPrecedingParanthetical()
        {
            string expression = "(!(1 || !2 && 3) || 4) || 5 && 6";

            List<Token> tokens = _testServices.TokenParser.ParseInfixExpression(expression);
            Stack<Token> postfixedTokens = _testServices.Postfixer.ConvertToPostfixStack(tokens);

            string expectedPostfixExpression = "1 2 ! 3 && || ! 4 || 5 6 && ||";

            Assert.AreEqual(expectedPostfixExpression, postfixedTokens.AsPostfixExpression());
        }
    }
}
