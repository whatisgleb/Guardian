using Guardian.Core.Interfaces;
using Guardian.Core.Tokens;

namespace Guardian.Core.ExpressionTree
{
    internal class ExpressionTreeNode
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
        /// <param name="leftID">ValidationConditionID Representation of the leftID child Identifier node.</param>
        /// <param name="rightID">ValidationConditionID Representation of the rightID child Identifier node.</param>
        public ExpressionTreeNode(IToken token, int leftID, int? rightID)
            : this(
                token, new ExpressionTreeNode(new IdentifierToken(leftID)),
                rightID.HasValue ? new ExpressionTreeNode(new IdentifierToken(rightID.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="rightID">ValidationConditionID Representation of the rightID child Identifier node.</param>
        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, int? rightID)
            : this(token, left, rightID.HasValue ? new ExpressionTreeNode(new IdentifierToken(rightID.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="leftID">ValidationConditionID Representation of the leftID child Identifier node.</param>
        /// <param name="right">Right child node.</param>
        public ExpressionTreeNode(IToken token, int leftID, ExpressionTreeNode right = null)
            : this(token, new ExpressionTreeNode(new IdentifierToken(leftID)), right) {}
    }
}