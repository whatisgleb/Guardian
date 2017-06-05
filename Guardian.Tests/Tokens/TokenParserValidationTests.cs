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
            string expression = "1 && && 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Consecutive_AndClosingParanthesisOperators()
        {
            string expression = "(1 &&) 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_IsMissing_OpeningParanthesis()
        {
            string expression = "1 && 2)";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_IsMissing_ClosingParanthesis()
        {
            string expression = "(1 && 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_IncompleteOperatorToken()
        {

            string expression = "1 & 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_InvalidOperatorToken()
        {

            string expression = "1 &| 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_AlphaCharacter()
        {
            string expression = "1 a 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_AdditionOperatorToken()
        {
            string expression = "1 + 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_DivisionOperatorToken()
        {
            string expression = "1 / 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_SubtractionOperatorToken()
        {
            string expression = "1 - 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_MultiplicationOperatorToken()
        {
            string expression = "1 * 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Expression_Contains_ModulusOperatorToken()
        {
            string expression = "1 % 2";

            TokenParser tokenParser = new TokenParser();

            tokenParser.ParseInfixExpression(expression);
        }
    }
}
