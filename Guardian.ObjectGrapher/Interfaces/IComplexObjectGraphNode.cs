using System.Collections.Generic;

namespace Guardian.ObjectGrapher.Interfaces
{
    internal interface IComplexObjectGraphNode : IObjectGraphNode
    {
        List<IObjectGraphNode> ChildrenObjectGraphNodes { get; set; }
    }
}