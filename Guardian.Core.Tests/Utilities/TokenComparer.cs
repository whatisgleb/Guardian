using System.Collections;
using System.Collections.Generic;
using Guardian.Core.Interfaces;

namespace Guardian.Core.Tests.Utilities
{
    /// <summary>
    /// Custom comparer to handle Token comparisons
    /// Operators are considered to be the same if their Types match
    /// Identifiers are considered to be the same if their IDs match
    /// </summary>
    internal class TokenComparer : IComparer, IComparer<IToken>
    {
        /// <summary>
        /// Compare specified Tokens
        /// </summary>
        /// <param name="x">Token</param>
        /// <param name="y">Token</param>
        /// <returns></returns>
        public int Compare(IToken x, IToken y)
        {
            if (x.GetType() != y.GetType()) return -1;

            if (x.GetType() == typeof(IIdentifier)) return ((IIdentifier) x).ID == ((IIdentifier) y).ID ? 0 : -1;

            if (x.GetType() == y.GetType()) return 0;

            // Something is wrong, specified Tokens must not match
            return -1;
        }

        /// <summary>
        /// Verify specified objects are Tokens and compare them
        /// </summary>
        /// <param name="x">Token</param>
        /// <param name="y">Token</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            IToken xToken = (IToken) x;
            IToken yToken = (IToken) y;

            return Compare(xToken, yToken);
        }
    }
}