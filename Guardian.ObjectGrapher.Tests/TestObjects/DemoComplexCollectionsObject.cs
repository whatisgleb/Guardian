using System.Collections.Generic;

namespace Guardian.ObjectGrapher.Tests.TestObjects
{
    public class DemoComplexCollectionsObject
    {
        public int DemoComplexCollectionsObjectID { get; set; }
        public string Description { get; set; }
        public DemoComplexObject DemoComplexObject { get; set; }
        public DemoObject DemoObject { get; set; }

        public IEnumerable<DemoComplexObject> DemoComplexObjects { get; set; }
    }
}