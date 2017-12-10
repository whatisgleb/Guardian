using Guardian.ObjectGrapher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guardian.ObjectGrapher
{
    internal static class ObjectGraphers
    {
        private static IEnumerable<IApplicableObjectGrapher> _availableObjectGraphers;
        public static IEnumerable<IApplicableObjectGrapher> AvailableObjectGraphers
        {
            get
            {
                return _availableObjectGraphers ?? (_availableObjectGraphers = new List<IApplicableObjectGrapher>()
                {
                    new GuardianPrimitiveObjectGrapher(),
                    new GuardianComplexObjectGrapher(),
                    new GuardianCollectionObjectGrapher()
                });
            }
        }

        public static IObjectGrapher GetApplicableObjectGrapher(Type type)
        {
            return AvailableObjectGraphers
                .Where(o => o.IsApplicable(type))
                .SingleOrDefault();
        }
    }
}