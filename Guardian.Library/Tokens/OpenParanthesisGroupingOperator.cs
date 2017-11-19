using System;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public class OpenParanthesisGroupingOperator : Operator
    {
        public override string StringRepresentation { get; } = "(";
        public override byte? Precedence { get; }

        public override bool Evaluate(params Func<bool>[] operands)
        {
            throw new Exception("Cannot evaluate a Grouping Operator");
        }
    }
}