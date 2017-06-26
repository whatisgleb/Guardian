﻿using Guardian.Library.ExpressionTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Tests.Utilities
{
    public class ExpressionTreeNodeComparer : IComparer, IComparer<ExpressionTreeNode>
    {
        public int Compare(object x, object y) {

            ExpressionTreeNode xNode = (ExpressionTreeNode) x;
            ExpressionTreeNode yNode = (ExpressionTreeNode) y;

            return Compare(xNode, yNode)
            ;
        }

        public int Compare(ExpressionTreeNode x, ExpressionTreeNode y) {

            if (x == null && y == null) return 0;

            if (x == null || y == null) return -1;

            if (x.Token.GetType() != y.Token.GetType()) return -1;

            if (x.Token.GetType() == typeof(IIdentifier)) {

                return ((IIdentifier) x.Token).ID == ((IIdentifier) y.Token).ID ? 0 : -1;
            }

            return Compare(x.Left, y.Left) == 0 && Compare(x.Right, y.Right) == 0 ? 0 : -1;
        }
    }
}
