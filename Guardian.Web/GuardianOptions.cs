using System;
using System.Collections.Generic;
using Guardian.Data;

namespace Guardian.Web
{
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
