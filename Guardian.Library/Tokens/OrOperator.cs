using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class OrOperator : Operator
    {
        public override string StringRepresentation { get; } = "||";
        public override byte? Precedence { get; } = 0;

        public override bool Evaluate(params Func<bool>[] operands)
        {
            if (operands.Length != 2)
            {
                throw new Exception("Unexpected number of operands while applying the Or Operator.");
            }

            return operands[0]() || operands[1]();
        }
    }
}