using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;
using Guardian.Data;

namespace Guardian.Web
{
    internal static class GuardianOptionsFactory
    {
        private static Func<GuardianOptions> _optionsFactory { get; set; }

        public static void RegisterOptionsFactory(Func<GuardianOptions> optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public static GuardianOptions GetOptions()
        {
            return _optionsFactory();
        }
    }

    public class GuardianOptions
    {
        public Func<GuardianDataProvider> GuardianDataProviderFactory { get; set; }
        public string ApplicationID { get; private set; }

        public GuardianOptions(string applicationID)
        {
            if (string.IsNullOrWhiteSpace(applicationID))
            {
                throw new ArgumentException($"Guardian requires an application identifier. You specified {applicationID} which is not valid.");
            }

            ApplicationID = applicationID;
        }
    }
}
