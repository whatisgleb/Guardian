using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;

namespace Guardian.Library.ExpressionTree
{
    public class ExpressionTreeNode
    {
        public IToken Token { get; set; }

        public ExpressionTreeNode Left { get; set; }
        public ExpressionTreeNode Right { get; set; }

        public ExpressionTreeNode() {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        public ExpressionTreeNode(IToken token)
        {
            Token = token;
        }

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, ExpressionTreeNode right)
        {
            Token = token;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">ID Representation of the left child Identifier node.</param>
        /// <param name="right">ID Representation of the right child Identifier node.</param>
        public ExpressionTreeNode(IToken token, int left, int? right)
            : this(
                token, new ExpressionTreeNode(new IdentifierToken(left)),
                right.HasValue ? new ExpressionTreeNode(new IdentifierToken(right.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">ID Representation of the right child Identifier node.</param>
        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, int? right)
            : this(token, left, right.HasValue ? new ExpressionTreeNode(new IdentifierToken(right.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">ID Representation of the left child Identifier node.</param>
        /// <param name="right">Right child node.</param>
        public ExpressionTreeNode(IToken token, int left, ExpressionTreeNode right = null)
            : this(token, new ExpressionTreeNode(new IdentifierToken(left)), right) {}
    }
}