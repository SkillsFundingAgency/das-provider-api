using System.Linq;
using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Sfa.Das.ApprenticeshipInfoService.Api.Attributes;
    using Sfa.Das.ApprenticeshipInfoService.Api.Helpers;
    using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
    using Sfa.Das.ApprenticeshipInfoService.Core.Models;
    using Sfa.Das.ApprenticeshipInfoService.Core.Models.Responses;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using SFA.DAS.Apprenticeships.Api.Types;
    using Swashbuckle.Swagger.Annotations;
    using IControllerHelper = Sfa.Das.ApprenticeshipInfoService.Core.Helpers.IControllerHelper;

    public class AssessmentOrgsController : ApiController
    {
        private readonly IGetAssessmentOrgs _getAssessmentOrgs;
        private readonly ILog _logger;

        public AssessmentOrgsController(
            IGetAssessmentOrgs getAssessmentOrgs,
            ILog logger)
        {
            _getAssessmentOrgs = getAssessmentOrgs;
            _logger = logger;
        }

        // GET /assessmentsorgs
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Organization>))]
        [Route("assessmentorgs")]
        [ExceptionHandling]
        public IEnumerable<OrganizationDTO> Get()
        {
            try
            {
                var response = _getAssessmentOrgs.GetAllOrganizations();

                foreach (var organization in response)
                {
                    organization.Uri = Resolve(organization.Id);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, "/assessment-organizations");
                throw;
            }
        }

        // GET /assessmentsorgs/{organizationId}
        [SwaggerOperation("GetOrganization")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Organization>))]
        [Route("assessmentorgs/{organizationId}")]
        [ExceptionHandling]
        public OrganizationDetailsDTO Get(string organizationId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganizationById(organizationId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organization with EpaOrganisationIdentifier {organizationId} found");
                }

                response.Uri = Resolve(response.EpaOrganisationIdentifier);

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"/assessment-organizations/{organizationId}");
                throw;
            }
        }

        // GET /assessmentsorgs/{organizationId}
        [SwaggerOperation("GetOrganization")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Organization>))]
        [Route("assessmentorgs/standards/{standardId}")]
        [ExceptionHandling]
        public IEnumerable<OrganizationDetailsDTO> GetByStandardId(string standardId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganizationsByStandardId(standardId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organization with EpaOrganisationIdentifier {standardId} found");
                }

                foreach (var organization in response)
                {
                    organization.Uri = Resolve(organization.EpaOrganisationIdentifier);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"/assessment-organizations/standards/{standardId}");
                throw;
            }
        }

        private string Resolve(string organizationId)
        {
            return Url.Link("DefaultApi", new { controller = "assessmentorgs", id = organizationId });
        }
    }
}