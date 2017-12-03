using System.Collections.Generic;

namespace Guardian.ObjectGrapher.Interfaces
{
    public interface IComplexObjectGraphNode : IObjectGraphNode
    {
        List<IObjectGraphNode> ChildrenObjectGraphNodes { get; set; }
    }
}