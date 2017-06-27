using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class OpenParanthesisGroupingOperator : Operator, IOperator
    {
        public string StringRepresentation { get; } = "(";
        public byte? Precedence { get; }

        public bool Evaluate(params Func<bool>[] operands)
        {
            throw new Exception("Cannot evaluate a Grouping Operator");
        }
    }
}