using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class NotOperator : Operator, IOperator
    {
        public string StringRepresentation { get; } = "!";
        public byte? Precedence { get; } = 2;

        public bool Evaluate(params Func<bool>[] operands)
        {
            if (operands.Length != 2)
            {
                // TODO: exception
                throw new Exception("Unexpected number of operands while applying the Not Operator.");
            }

            return !operands[0]();
        }
    }
}