using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Data
{
    public static class GuardianDataConfiguration
    {
        private static Func<GuardianDataProvider> _dataProviderFactory { get; set; }

        public static void RegisterDataProvider(Func<GuardianDataProvider> dataProviderFactory)
        {
            _dataProviderFactory = dataProviderFactory;
        }

        public static GuardianDataProvider GetDataProvider()
        {
            return _dataProviderFactory();
        }
    }
}
