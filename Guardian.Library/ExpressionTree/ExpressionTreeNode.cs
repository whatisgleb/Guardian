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
        /// <param name="leftRuleID">RuleID Representation of the leftRuleID child Identifier node.</param>
        /// <param name="rightRuleID">RuleID Representation of the rightRuleID child Identifier node.</param>
        public ExpressionTreeNode(IToken token, int leftRuleID, int? rightRuleID)
            : this(
                token, new ExpressionTreeNode(new IdentifierToken(leftRuleID)),
                rightRuleID.HasValue ? new ExpressionTreeNode(new IdentifierToken(rightRuleID.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="rightRuleID">RuleID Representation of the rightRuleID child Identifier node.</param>
        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, int? rightRuleID)
            : this(token, left, rightRuleID.HasValue ? new ExpressionTreeNode(new IdentifierToken(rightRuleID.Value)) : null) {}

        /// <summary>
        /// Convenience constructor to enable easy to write Unit Tests.
        /// </summary>
        /// <param name="token">Token Representation of the current node.</param>
        /// <param name="leftRuleID">RuleID Representation of the leftRuleID child Identifier node.</param>
        /// <param name="right">Right child node.</param>
        public ExpressionTreeNode(IToken token, int leftRuleID, ExpressionTreeNode right = null)
            : this(token, new ExpressionTreeNode(new IdentifierToken(leftRuleID)), right) {}
    }
}