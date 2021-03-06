﻿using System;

namespace Guardian.Core.Tokens
{
    internal class OpenParanthesisGroupingOperator : Operator
    {
        public override string StringRepresentation { get; } = "(";
        public override byte? Precedence { get; }

        public override bool Evaluate(params Func<bool>[] operands)
        {
            throw new Exception("Cannot evaluate a Grouping Operator");
        }
    }
}