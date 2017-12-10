using Guardian.ObjectGrapher.Extensions;
using Guardian.ObjectGrapher.Interfaces;
using Guardian.ObjectGrapher.Nodes;
using System;

namespace Guardian.ObjectGrapher
{
    internal class GuardianPrimitiveObjectGrapher : IApplicableObjectGrapher
    {
        public bool IsApplicable(Type type)
        {
            return !type.IsComplexType();
        }

        public IObjectGraphNode BuildObjectGraph(Type type, string nodeName)
        {
            return new ObjectGraphNode()
            {
                NodeType = type,
                NodeName = nodeName
            };
        }
    }
}