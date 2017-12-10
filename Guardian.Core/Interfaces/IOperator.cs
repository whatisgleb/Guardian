using System;

namespace Guardian.Core.Interfaces
{
    public interface IOperator : IToken
    {
        string StringRepresentation { get; }
        byte? Precedence { get; }

        bool Evaluate(params Func<bool>[] operands);
    }
}