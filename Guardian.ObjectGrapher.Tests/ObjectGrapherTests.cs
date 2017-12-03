using System;
using System.Collections.Generic;
using System.Linq;
using Guardian.ObjectGrapher.Interfaces;
using Guardian.ObjectGrapher.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.ObjectGrapher.Tests
{
    [TestClass]
    public class ObjectGrapherTests
    {
        private IObjectGrapher _objectGrapher;

        [TestInitialize]
        public void TestInitialize()
        {
            _objectGrapher = new GuardianObjectGrapher();
        }

        [TestMethod]
        public void Given_PrimitiveType_Expect_RootNode()
        {
            // Arrange
            Type type = typeof(int);
            string rootNodeName = "Int";

            // Act
            IObjectGraphNode node = _objectGrapher.BuildObjectGraph(type, rootNodeName);

            // Assert
            Assert.IsNotNull(node, "Expected an object graph for specified parameter.");
            Assert.IsTrue(node.NodeType == type);
            Assert.IsTrue(node.NodeName == rootNodeName);
        }

        [TestMethod]
        public void Given_ComplexType_Expect_RootNode_WithChildNodes()
        {
            // Arrange
            Type type = typeof(DemoObject);
            string rootNodeName = "DemoObject";

            // Act
            IComplexObjectGraphNode node = (IComplexObjectGraphNode)_objectGrapher.BuildObjectGraph(type, rootNodeName);

            // Assert
            Assert.IsTrue(node.ChildrenObjectGraphNodes.Any());
        }

        [TestMethod]
        public void Given_ComplexType_With_ComplexChildren_Expect_RootNode_WithDeepChildNodes()
        {
            // Arrange
            Type type = typeof(DemoComplexObject);
            string rootNodeName = "DemoComplexObject";

            // Act
            IComplexObjectGraphNode node = (IComplexObjectGraphNode)_objectGrapher.BuildObjectGraph(type, rootNodeName);

            // Assert
            IEnumerable<IComplexObjectGraphNode> complexChildrenNodes = node.ChildrenObjectGraphNodes
                .Where(o => o is IComplexObjectGraphNode)
                .Select(o => (IComplexObjectGraphNode)o)
                .ToList();

            Assert.IsTrue(complexChildrenNodes.Any());
        }

        [TestMethod]
        public void Given_ComplexType_With_ComplexChildrenCollection_Expect_RootNode_WithDeepChildNodes()
        {
            // Arrange
            Type type = typeof(DemoComplexCollectionsObject);
            string rootNodeName = "DemoComplexCollectionsObject";

            // Act
            IComplexObjectGraphNode node = (IComplexObjectGraphNode)_objectGrapher.BuildObjectGraph(type, rootNodeName);

            // Assert
            IComplexObjectGraphNode complexNode = node.ChildrenObjectGraphNodes
                .Where(o => o is IComplexObjectGraphNode)
                .Select(o => (IComplexObjectGraphNode)o)
                .Where(o => o.NodeName == "DemoComplexObjects")
                .SingleOrDefault();
            
            Assert.IsTrue(complexNode.ChildrenObjectGraphNodes.Count == 1, "Expected collection node to have one child which represents the type of object in the collection.");
            Assert.IsTrue(complexNode.ChildrenObjectGraphNodes.First().NodeType == typeof(DemoComplexObject));
        }
    }
}