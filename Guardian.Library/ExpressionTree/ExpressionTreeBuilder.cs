using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Interfaces;
using Guardian.Library.Postfix;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.ExpressionTree
{
    public class ExpressionTreeBuilder
    {
        public ExpressionTreeNode BuildExpressionTree(Stack<Token> tokens)
        {
            Stack<Token> reverseTokens = new Stack<Token>(tokens);

            ExpressionTreeNode root = GenerateNode(reverseTokens);

            return root;
        }

        private ExpressionTreeNode GenerateNode(Stack<Token> tokens)
        {
            ExpressionTreeNode treeNode = new ExpressionTreeNode();
            Token currentToken = tokens.Pop();

            if (currentToken.IsOperatorToken())
            {
                treeNode.Token = currentToken;

                if (((OperatorToken) currentToken).Type != OperatorTypeEnum.Not)
                {
                    if (tokens.Peek().IsIdentifierToken())
                    {
                        // Right Node
                        treeNode.Right = new ExpressionTreeNode()
                        {
                            Token = tokens.Pop()
                        };
                    }
                    else
                    {
                        treeNode.Right = GenerateNode(tokens);
                    }
                }

                if (tokens.Peek().IsIdentifierToken())
                {
                    treeNode.Left = new ExpressionTreeNode()
                    {
                        Token = tokens.Pop()
                    };
                }
                else
                {
                    treeNode.Left = GenerateNode(tokens);
                }
            }

            if (currentToken.IsIdentifierToken())
            {
                treeNode.Token = currentToken;
            }

            return treeNode;
        }
    }
}