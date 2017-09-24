using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;
using Guardian.Data;
using Guardian.Web.Abstractions;
using Guardian.Web.Implementations;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers.Api
{
    [RoutePrefix("api/validations")]
    internal class GuardianValidationApiController
    {
        [Route("")]
        public IResponse GetValidations()
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            return new Json(dataProvider.GetValidations(options.ApplicationID));
        }

        [Route("", HTTP.POST)]
        public IResponse CreateValidation(Validation validation)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            IValidation createdValidation = dataProvider.CreateValidation(validation);

            return new Json(dataProvider.GetValidation(createdValidation.ValidationID));
        }
    }
}
