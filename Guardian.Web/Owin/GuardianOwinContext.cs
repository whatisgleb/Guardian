using Guardian.Web.Abstractions;
using System;
using System.Collections.Generic;

namespace Guardian.Web.Owin
{
    internal sealed class GuardianOwinContext : GuardianContext
    {
        internal GuardianOwinContext(IDictionary<string, object> environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            Environment = environment;

            Request = new GuardianOwinRequest(environment);
            Response = new GuardianOwinResponse(environment);
        }

        public IDictionary<string, object> Environment { get; }
    }
}
