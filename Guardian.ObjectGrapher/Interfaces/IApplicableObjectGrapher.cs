using System;

namespace Guardian.ObjectGrapher.Interfaces
{
    internal interface IApplicableObjectGrapher : IObjectGrapher
    {
        bool IsApplicable(Type type);
    }
}