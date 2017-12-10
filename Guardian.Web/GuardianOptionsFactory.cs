using System;
using System.Collections.Generic;
using System.Linq;
using Guardian.ObjectGrapher;
using Guardian.ObjectGrapher.Interfaces;

namespace Guardian.Web
{
    internal static class GuardianOptionsFactory
    {
        private static Func<GuardianOptions> _optionsFactory { get; set; }
        private static IEnumerable<IObjectGraphNode> _registeredObjectGraphNodes { get; set; }

        public static void RegisterOptionsFactory(Func<GuardianOptions> optionsFactory)
        {
            _optionsFactory = optionsFactory;

            // Build registered object graphs
            GuardianOptions options = GetOptions();
            GuardianObjectGrapher objectGrapher = new GuardianObjectGrapher();

            _registeredObjectGraphNodes = options.TypesToValidate
                .Select(t => objectGrapher.BuildObjectGraph(t, t.Name))
                .ToList();
        }

        public static GuardianOptions GetOptions()
        {
            return _optionsFactory();
        }

        public static IEnumerable<IObjectGraphNode> GetRegisteredObjectGraphNodes()
        {
            return _registeredObjectGraphNodes;
        }
    }
}