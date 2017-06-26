using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens.Operators
{
    public class OrOperator : Operator, IOperator
    {
        public string StringRepresentation { get; } = "||";
        public byte? Precedence { get; } = 0;

        public bool Evaluate(params Func<bool>[] operands)
        {
            if (operands.Length != 2)
            {
                throw new Exception("Unexpected number of operands while applying the Or Operator.");
            }

            return operands[0]() || operands[1]();
        }
    }
}