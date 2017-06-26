using System;

namespace Guardian.Library.Interfaces
{
    public interface IOperator : IToken
    {
        string StringRepresentation { get; }
        byte? Precedence { get; }

        bool Evaluate(params Func<bool>[] operands);
    }

    public interface IIdentifier : IToken
    {
        int ID { get; }
    }
}