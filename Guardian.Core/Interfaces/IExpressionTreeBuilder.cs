using System.Collections.Generic;
using Guardian.Core.ExpressionTree;

namespace Guardian.Core.Interfaces
{
    internal interface IExpressionTreeBuilder
    {
        ExpressionTreeNode BuildExpressionTree(Stack<IToken> tokens);
    }
}