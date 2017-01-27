﻿namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Sfa.Das.ApprenticeshipInfoService.Api.Attributes;
    using Sfa.Das.ApprenticeshipInfoService.Api.Helpers;
    using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
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

        // GET /assessment-organisations
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<OrganisationSummary>))]
        [Route("assessment-organisations")]
        [ExceptionHandling]
        public IEnumerable<OrganisationSummary> Get()
        {
            try
            {
                var response = _getAssessmentOrgs.GetAllOrganisations().ToList();

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

        // GET /assessment-organisations/{organisationId}
        [SwaggerOperation("GetOrganisation")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Organisation))]
        [Route("assessment-organisations/{organisationId}")]
        [ExceptionHandling]
        public Organisation Get(string organisationId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganisationById(organisationId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organisation with EpaOrganisationIdentifier {organisationId} found");
                }

                response.Uri = Resolve(response.Id);

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"/assessment-organisations/{organisationId}");
                throw;
            }
        }

        // HEAD /assessment-organisations/10005318
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("assessment-organisations/{organisationId}")]
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

        // GET /assessment-organisations/standards/{organisationId}
        [SwaggerOperation("GetByStandard")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Organisation>))]
        [Route("assessment-organisations/standards/{standardId}")]
        [ExceptionHandling]
        public IEnumerable<Organisation> GetByStandardId(string standardId)
        {
            try
            {
                var response = _getAssessmentOrgs.GetOrganisationsByStandardId(standardId);

                if (response == null)
                {
                    throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                        $"No organisation with EpaOrganisationIdentifier {standardId} found");
                }

                response = response.ToList();
                foreach (var organisation in response)
                {
                    organisation.Uri = Resolve(organisation.Id);
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
            return Url.Link("DefaultApi", new { controller = "assessmentorgs", id = organisationId }).Replace("assessmentorgs", "assessment-organisations");
        }
    }
}