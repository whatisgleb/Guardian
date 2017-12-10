using Guardian.ObjectGrapher.Extensions;
using Guardian.ObjectGrapher.Interfaces;
using Guardian.ObjectGrapher.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guardian.ObjectGrapher
{
    internal class GuardianCollectionObjectGrapher : IApplicableObjectGrapher
    {
        private Dictionary<Type, List<IObjectGraphNode>> _cachedNodes;

        public GuardianCollectionObjectGrapher()
        {
            _cachedNodes = new Dictionary<Type, List<IObjectGraphNode>>();
        }

        public bool IsApplicable(Type type)
        {
            return type.IsCollectionType();
        }

        public IObjectGraphNode BuildObjectGraph(Type type, string nodeName)
        {
            ComplexObjectGraphNode node = new ComplexObjectGraphNode()
            {
                NodeType = type,
                NodeName = nodeName
            };

            if (_cachedNodes.ContainsKey(type))
            {
                node.ChildrenObjectGraphNodes = _cachedNodes[type];
                return node;
            }

            Type genericType = type.GenericTypeArguments.SingleOrDefault();

            IObjectGrapher objectGrapher = ObjectGraphers.GetApplicableObjectGrapher(genericType);
            IObjectGraphNode childNode = objectGrapher?.BuildObjectGraph(genericType, genericType.Name);

            node.ChildrenObjectGraphNodes.Add(childNode);

            _cachedNodes.Add(type, node.ChildrenObjectGraphNodes);

            return node;
        }
    }
}