using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Extensions;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.ExpressionConversions
{
    [TestClass]
    public class PostfixStackToExpressionTests
    {
        [TestMethod]
        public void ToExpression_FromTokenStack() {
            
            // Arrange
            Stack<Token> tokenStack = new Stack<Token>();
            tokenStack.Push(new OperatorToken(OperatorTypeEnum.And));
            tokenStack.Push(new OperatorToken(OperatorTypeEnum.Not));
            tokenStack.Push(new IdentifierToken(2));
            tokenStack.Push(new IdentifierToken(1));

            // Act
            string postfixExpression = tokenStack.AsPostfixExpression();

            // Assert
            string expectedPostfixExpression = "1 2 ! &&";

            Assert.AreEqual(expectedPostfixExpression, postfixExpression);
        }
    }
}
