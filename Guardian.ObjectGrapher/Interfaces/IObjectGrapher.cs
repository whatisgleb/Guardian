using System;

namespace Guardian.ObjectGrapher.Interfaces
{
    public interface IObjectGrapher
    {
        IObjectGraphNode BuildObjectGraph(Type type, string nodeName);
    }
}