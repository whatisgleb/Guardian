using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;
using Guardian.Data;
using Guardian.Web.Implementations;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Enums;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers.Api
{

    [RoutePrefix("api/validation-conditions")]
    internal class GuardianValidationConditionApiController
    {

        [Route("")]
        public IResponse GetValidationConditions()
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            return new JsonResponse(dataProvider.GetValidationConditions(options.ApplicationID));
        }

        [Route("", HttpRequestMethod.POST)]
        public IResponse CreateValidationCondition(ValidationCondition validationCondition)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            IValidationCondition createdValidation = dataProvider.CreateValidationCondition(validationCondition);

            return new JsonResponse(dataProvider.GetValidation(createdValidation.ValidationConditionID));
        }

        [Route("", HttpRequestMethod.PUT)]
        public IResponse UpdateValidationCondition(ValidationCondition validationCondition)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            IValidationCondition createdValidation = dataProvider.UpdateValidationCondition(validationCondition);

            return new JsonResponse(dataProvider.GetValidation(createdValidation.ValidationConditionID));
        }

        [Route("{validationConditionID}")]
        public IResponse GetValidationCondition(int validationConditionID)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();

            return new JsonResponse(dataProvider.GetValidationCondition(validationConditionID));
        }

        [Route("{validationConditionID}", HttpRequestMethod.DELETE)]
        public IResponse DeleteValidationCondition(int validationConditionID)
        {
            GuardianOptions options = GuardianOptionsFactory.GetOptions();
            GuardianDataProvider dataProvider = options.GuardianDataProviderFactory();
            dataProvider.DeleteValidationCondition(validationConditionID);
            return new JsonResponse(string.Empty);
        }
    }
}
