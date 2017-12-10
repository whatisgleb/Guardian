using Guardian.ObjectGrapher.Interfaces;
using System;

namespace Guardian.ObjectGrapher.Nodes
{
    public class ObjectGraphNode : IObjectGraphNode
    {
        public string NodeName { get; set; }
        public Type NodeType { get; set; }
    }
}
