using System;

namespace Guardian.ObjectGrapher.Interfaces
{
    public interface IApplicableObjectGrapher : IObjectGrapher
    {
        bool IsApplicable(Type type);
    }
}