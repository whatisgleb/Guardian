using Guardian.Data;
using Guardian.ObjectGrapher;
using Guardian.ObjectGrapher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public class GuardianOptions
    {
        public Func<GuardianDataProvider> GuardianDataProviderFactory { get; set; }
        public string ApplicationID { get; private set; }
        public List<Type> TypesToValidate { get; set; } //TODO: Is there a better way to include types to validate?

        public GuardianOptions(string applicationID)
        {
            if (string.IsNullOrWhiteSpace(applicationID))
            {
                throw new ArgumentException($"Guardian requires an application identifier. You specified '{applicationID}' which is not valid.");
            }

            ApplicationID = applicationID;
            TypesToValidate = new List<Type>();
        }
    }
}
