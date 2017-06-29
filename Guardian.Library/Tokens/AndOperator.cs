using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class AndOperator : Operator, IOperator
    {
        public string StringRepresentation { get; } = "&&";
        public byte? Precedence { get; } = 1;

        public bool Evaluate(params Func<bool>[] operands)
        {
            if (operands.Length != 2)
            {
                throw new Exception("Unexpected number of operands while applying the And Operator.");
            }

            return operands[0]() && operands[1]();
        }
    }
}