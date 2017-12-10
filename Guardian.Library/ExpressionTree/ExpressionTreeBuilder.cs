using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using System.Collections.Generic;

namespace Guardian.Library.ExpressionTree
{
    public class ExpressionTreeBuilder
    {
        /// <summary>
        /// Converts specified Token Stack into an expression tree.
        /// </summary>
        /// <param name="tokens">Token representation of a logical expression.</param>
        /// <returns></returns>
        public ExpressionTreeNode BuildExpressionTree(Stack<IToken> tokens)
        {
            Stack<IToken> reverseTokens = new Stack<IToken>(tokens);

            ExpressionTreeNode root = GenerateNode(reverseTokens);

            return root;
        }

        /// <summary>
        /// Iterates over specified tokens to recursively build an expression tree node.
        /// </summary>
        /// <param name="tokens">Token representation of a logical expression.</param>
        /// <returns></returns>
        private ExpressionTreeNode GenerateNode(Stack<IToken> tokens)
        {
            ExpressionTreeNode treeNode = new ExpressionTreeNode();
            IToken currentToken = tokens.Pop();

            if (currentToken.IsOperatorToken())
            {
                treeNode.Token = currentToken;

                if (currentToken.GetType() != typeof(NotOperator))
                {
                    treeNode.Right = GenerateNode(tokens);
                }

                treeNode.Left = GenerateNode(tokens);
            }
            else
            {
                treeNode.Token = currentToken;
            }

            return treeNode;
        }
    }
}