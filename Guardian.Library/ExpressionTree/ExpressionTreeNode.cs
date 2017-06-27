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

        public ExpressionTreeNode()
        {
            
        }

        public ExpressionTreeNode(IToken token)
        {
            Token = token;
        }

        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, ExpressionTreeNode right)
        {
            Token = token;
            Left = left;
            Right = right;
        }

        public ExpressionTreeNode(IToken token, int left, int? right)
            : this(
                token, new ExpressionTreeNode(new IdentifierToken(left)),
                right.HasValue ? new ExpressionTreeNode(new IdentifierToken(right.Value)) : null)
        {
            
        }

        public ExpressionTreeNode(IToken token, ExpressionTreeNode left, int? right)
            : this(token, left, right.HasValue ? new ExpressionTreeNode(new IdentifierToken(right.Value)) : null)
        {
            
        }

        public ExpressionTreeNode(IToken token, int left, ExpressionTreeNode right = null)
            : this(token, new ExpressionTreeNode(new IdentifierToken(left)), right)
        {
            
        }
    }
}
