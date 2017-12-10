using Guardian.ObjectGrapher.Interfaces;
using System;

namespace Guardian.ObjectGrapher
{
    public class GuardianObjectGrapher : IObjectGrapher
    {
        public IObjectGraphNode BuildObjectGraph(Type type, string nodeName)
        {
            IObjectGrapher objectGrapher = ObjectGraphers.GetApplicableObjectGrapher(type);

            return objectGrapher?.BuildObjectGraph(type, nodeName);
        }
    }
}
