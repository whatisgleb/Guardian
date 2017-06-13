using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.ExpressionTree;
using Guardian.Library.Postfix;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests.Tokens
{
    [TestClass]
    public class ExpressionBuilderTests
    {
        private TestServices _testServices;

        public ExpressionBuilderTests()
        {

            _testServices = new TestServices();
        }

        [TestMethod]
        public void And_ExpressionTree() {
            
            // Arrange
            string expression = "1 && 2";
            Stack<Token> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode() {
                
                Token = new OperatorToken(OperatorTypeEnum.And),
                Left = new ExpressionTreeNode() {
                    
                    Token = new IdentifierToken(1)
                },
                Right = new ExpressionTreeNode() {
                    
                    Token = new IdentifierToken(2)
                }
            };

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
        }

        [TestMethod]
        public void AndAnd_ExpressionTree()
        {

            // Arrange
            string expression = "1 && 2 && 3";
            Stack<Token> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode()
            {

                Token = new OperatorToken(OperatorTypeEnum.And),
                Right = new ExpressionTreeNode()
                {

                    Token = new OperatorToken(OperatorTypeEnum.And),
                    Right = new ExpressionTreeNode()
                    {

                        Token = new IdentifierToken(3)
                    },
                    Left = new ExpressionTreeNode()
                    {

                        Token = new IdentifierToken(2)
                    }
                },
                Left = new ExpressionTreeNode()
                {
                    Token = new IdentifierToken(1)
                }
            };

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
        }

        [TestMethod]
        public void AndOr_ExpressionTree()
        {

            // Arrange
            string expression = "1 && 2 || 3";
            Stack<Token> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode()
            {

                Token = new OperatorToken(OperatorTypeEnum.Or),
                Right = new ExpressionTreeNode()
                {

                    Token = new IdentifierToken(3),
                },
                Left = new ExpressionTreeNode()
                {
                    Token = new OperatorToken(OperatorTypeEnum.And),
                    Right = new ExpressionTreeNode()
                    {

                        Token = new IdentifierToken(2)
                    },
                    Left = new ExpressionTreeNode()
                    {

                        Token = new IdentifierToken(1)
                    }
                }
            };

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
        }


        [TestMethod]
        public void OrAnd_ExpressionTree()
        {

            // Arrange
            string expression = "1 || 2 && 3";
            Stack<Token> postfixTokens = _testServices.PostfixConverter.ConvertToStack(expression);

            // Act
            ExpressionTreeNode root = _testServices.ExpressionTreeBuilder.BuildExpressionTree(postfixTokens);

            // Assert
            ExpressionTreeNode expected = new ExpressionTreeNode()
            {

                Token = new OperatorToken(OperatorTypeEnum.Or),
                Right = new ExpressionTreeNode()
                {

                    Token = new OperatorToken(OperatorTypeEnum.And),
                    Right = new ExpressionTreeNode()
                    {
                        Token = new IdentifierToken(3)
                    },
                    Left = new ExpressionTreeNode()
                    {
                        Token = new IdentifierToken(2)
                    }
                },
                Left = new ExpressionTreeNode()
                {
                    Token = new IdentifierToken(1),
                    
                }
            };

            bool areEqual = new ExpressionTreeNodeComparer().Compare(expected, root) == 0;

            Assert.AreEqual(true, areEqual);
        }
    }
}
