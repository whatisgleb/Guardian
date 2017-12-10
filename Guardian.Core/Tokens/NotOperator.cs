using System;

namespace Guardian.Core.Tokens
{
    internal class NotOperator : Operator
    {
        public override string StringRepresentation { get; } = "!";
        public override byte? Precedence { get; } = 2;

        public override bool Evaluate(params Func<bool>[] operands)
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