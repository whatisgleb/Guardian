using System;

namespace Guardian.ObjectGrapher.Interfaces
{
    public interface IObjectGraphNode
    {
        string NodeName { get; set; }
        Type NodeType { get; set; }
    }
}