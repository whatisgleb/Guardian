using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Extensions;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.ExpressionConversions
{
    [TestClass]
    public class PostfixStackToExpressionTests
    {
        [TestMethod]
        public void ToExpression_FromTokenStack()
        {
            // Arrange
            Stack<IToken> tokenStack = new Stack<IToken>();
            tokenStack.Push(Operators.And);
            tokenStack.Push(Operators.Not);
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