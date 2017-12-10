using System;
using Guardian.Core.Interfaces;

namespace Guardian.Core.Tokens
{
    public abstract class Operator : IOperator
    {
        public bool IsOperatorToken()
        {
            return true;
        }

        public abstract string StringRepresentation { get; }
        public abstract byte? Precedence { get; }
        public abstract bool Evaluate(params Func<bool>[] operands);
    }
}