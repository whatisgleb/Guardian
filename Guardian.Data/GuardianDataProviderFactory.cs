using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;

namespace Guardian.Data
{
    public static class GuardianDataProviderFactory
    {
        private static Func<GuardianDataProvider> _dataProviderFactory { get; set; }

        public static void RegisterDataProviderFactory(Func<GuardianDataProvider> dataProviderFactory)
        {
            _dataProviderFactory = dataProviderFactory;
        }

        public static GuardianDataProvider GetDataProvider()
        {
            return _dataProviderFactory();
        }
    }
}
