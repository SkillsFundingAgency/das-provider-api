﻿namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using Sfa.Das.ApprenticeshipInfoService.Api.Attributes;
    using Sfa.Das.ApprenticeshipInfoService.Api.Helpers;
    using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using SFA.DAS.Apprenticeships.Api.Types;
    using SFA.DAS.Apprenticeships.Api.Types.DTOs;
    using Swashbuckle.Swagger.Annotations;

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
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<OrganisationDTO>))]
        [Route("assessmentorgs")]
        [ExceptionHandling]
        public IEnumerable<OrganisationDTO> Get()
        {
            try
            {
                var response = _getAssessmentOrgs.GetAllOrganisations();

                foreach (var organisation in response)
                {
                    organisation.Uri = Resolve(organisation.Id);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, "/assessment-organisations");
                throw;
            }
        }

        // GET /assessmentsorgs/{organisationId}
        [SwaggerOperation("GetOrganisation")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(OrganisationDetailsDTO))]
        [Route("assessmentorgs/{organisationId}")]
        [ExceptionHandling]
        public OrganisationDetailsDTO Get(string organisationId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganisationById(organisationId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organisation with EpaOrganisationIdentifier {organisationId} found");
                }

                response.Uri = Resolve(response.EpaOrganisationIdentifier);

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"/assessment-organisations/{organisationId}");
                throw;
            }
        }

        // HEAD /assessmentsorgs/10005318
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("assessmentorgs/{organisationId}")]
        [ExceptionHandling]
        public void Head(string organisationId)
        {
            if (_getAssessmentOrgs.GetOrganisationById(organisationId) != null)
            {
                return;
            }

            throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                $"No organisation with EpaOrganisationIdentifier {organisationId} found");
        }

        // GET /assessmentsorgs/{organisationId}
        [SwaggerOperation("GetOrganisation")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Organisation>))]
        [Route("assessmentorgs/standards/{standardId}")]
        [ExceptionHandling]
        public IEnumerable<OrganisationDetailsDTO> GetByStandardId(string standardId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganisationsByStandardId(standardId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organisation with EpaOrganisationIdentifier {standardId} found");
                }

                foreach (var organisation in response)
                {
                    organisation.Uri = Resolve(organisation.EpaOrganisationIdentifier);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"/assessment-organisations/standards/{standardId}");
                throw;
            }
        }

        private string Resolve(string organisationId)
        {
            return Url.Link("DefaultApi", new { controller = "assessmentorgs", id = organisationId });
        }
    }
}