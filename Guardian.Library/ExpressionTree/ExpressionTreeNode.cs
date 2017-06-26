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
    }
}
