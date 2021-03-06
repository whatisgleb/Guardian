﻿using Guardian.Common.Interfaces;
using Guardian.Data;
using Guardian.Web.Implementations;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Enums;
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

            return new JsonResponse(dataProvider.GetValidations(options.ApplicationID));
        }

        [Route("", HttpRequestMethod.POST)]
        public IResponse CreateValidation(Validation validation)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            IValidation createdValidation = dataProvider.CreateValidation(validation);

            return new JsonResponse(dataProvider.GetValidation(createdValidation.ValidationID));
        }

        [Route("", HttpRequestMethod.PUT)]
        public IResponse UpdateValidation(Validation validation)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            IValidation createdValidation = dataProvider.UpdateValidation(validation);

            return new JsonResponse(dataProvider.GetValidation(createdValidation.ValidationID));
        }

        [Route("{validationID}")]
        public IResponse GetValidation(int validationID)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            return new JsonResponse(dataProvider.GetValidation(validationID));
        }

        [Route("{validationID}", HttpRequestMethod.DELETE)]
        public IResponse DeleteValidation(int validationID)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();
            dataProvider.DeleteValidation(validationID);
            return new JsonResponse(string.Empty);
        }
    }
}
