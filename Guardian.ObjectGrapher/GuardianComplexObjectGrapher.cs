using System;
using System.Collections.Generic;
using System.Reflection;
using Guardian.ObjectGrapher.Extensions;
using Guardian.ObjectGrapher.Interfaces;
using Guardian.ObjectGrapher.Nodes;

namespace Guardian.ObjectGrapher
{
    internal class GuardianComplexObjectGrapher : IApplicableObjectGrapher
    {
        private Dictionary<Type, List<IObjectGraphNode>> _cachedNodes;

        public GuardianComplexObjectGrapher()
        {
            _cachedNodes = new Dictionary<Type, List<IObjectGraphNode>>();
        }

        public bool IsApplicable(Type type)
        {
            return !type.IsCollectionType() && type.IsComplexType();
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

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IObjectGrapher objectGrapher = ObjectGraphers.GetApplicableObjectGrapher(propertyInfo.PropertyType);

                IObjectGraphNode childNode = objectGrapher.BuildObjectGraph(propertyInfo.PropertyType, propertyInfo.Name);

                node.ChildrenObjectGraphNodes.Add(childNode);
            }

            _cachedNodes.Add(type, node.ChildrenObjectGraphNodes);

            return node;
        }
    }
}