using System;

namespace Guardian.ObjectGrapher.Interfaces
{
    public interface IObjectGrapher
    {
        IObjectGraphNode BuildObjectGraph(Type type, string nodeName);
    }

    public interface IApplicableObjectGrapher : IObjectGrapher
    {
        bool IsApplicable(Type type);
    }
}