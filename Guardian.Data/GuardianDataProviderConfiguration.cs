using System;

namespace Guardian.Data
{
    public static class GuardianDataProviderConfiguration
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
