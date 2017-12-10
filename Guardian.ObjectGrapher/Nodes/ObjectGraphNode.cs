using System;
using Guardian.ObjectGrapher.Interfaces;

namespace Guardian.ObjectGrapher.Nodes
{
    public class ObjectGraphNode : IObjectGraphNode
    {
        public string NodeName { get; set; }
        public Type NodeType { get; set; }
    }
}
