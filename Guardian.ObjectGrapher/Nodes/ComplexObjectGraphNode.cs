using System;
using System.Collections.Generic;
using Guardian.ObjectGrapher.Interfaces;

namespace Guardian.ObjectGrapher.Nodes
{
    public class ComplexObjectGraphNode : IComplexObjectGraphNode
    {
        public string NodeName { get; set; }
        public Type NodeType { get; set; }
        public List<IObjectGraphNode> ChildrenObjectGraphNodes { get; set; }

        public ComplexObjectGraphNode()
        {
            ChildrenObjectGraphNodes = new List<IObjectGraphNode>();
        }
    }
}