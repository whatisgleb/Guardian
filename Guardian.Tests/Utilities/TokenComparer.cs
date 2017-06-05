using System;
using System.Collections;
using System.Collections.Generic;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Tests.Utilities
{
    /// <summary>
    /// Custom comparer to handle Token comparisons
    /// Operators are considered to be the same if their Types match
    /// Identifiers are considered to be the same if their IDs match
    /// </summary>
    public class TokenComparer : IComparer, IComparer<Token>
    {
        /// <summary>
        /// Compare specified Tokens
        /// </summary>
        /// <param name="x">Token</param>
        /// <param name="y">Token</param>
        /// <returns></returns>
        public int Compare(Token x, Token y) {

            if (x.GetType() != y.GetType()) return -1;

            if (x.GetType() == typeof(Operator)) return ((Operator) x).Type == ((Operator) y).Type ? 0 : -1;

            if (x.GetType() == typeof(Identifier)) return ((Identifier) x).ID == ((Identifier) y).ID ? 0 : -1;

            // Something is wrong, specified Tokens must not match
            return -1;
        }

        /// <summary>
        /// Verify specified objects are Tokens and compare them
        /// </summary>
        /// <param name="x">Token</param>
        /// <param name="y">Token</param>
        /// <returns></returns>
        public int Compare(object x, object y) {

            Token xToken = (Token) x;
            Token yToken = (Token) y;

            return Compare(xToken, yToken);
        }
    }
}
