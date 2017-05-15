﻿using System.Web.Http.Description;

namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Sfa.Das.ApprenticeshipInfoService.Api.Attributes;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using SFA.DAS.Apprenticeships.Api.Types;
    using Swashbuckle.Swagger.Annotations;

    public class FrameworksController : ApiController
    {
        private readonly IGetFrameworks _getFrameworks;

        public FrameworksController(IGetFrameworks getFrameworks)
        {
            _getFrameworks = getFrameworks;
        }

        /// <summary>
        /// Get all the active frameworks
        /// </summary>
        /// <returns>a collection of frameworks</returns>
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<FrameworkSummary>))]
        [Route("frameworks")]
        [ExceptionHandling]
        public IEnumerable<FrameworkSummary> Get()
        {
            var response = _getFrameworks.GetAllFrameworks().ToList();

            foreach (var item in response)
            {
                item.Uri = Resolve(item.Id);
            }

            return response;
        }

        /// <summary>
        /// Get a framework
        /// </summary>
        /// <param name="id">{FrameworkId}-{ProgType}-{PathwayId}</param>
        /// <returns>a framework</returns>
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Framework))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("frameworks/{id}")]
        [ExceptionHandling]
        public Framework Get(string id)
        {
            var response = _getFrameworks.GetFrameworkById(id);

            if (response == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            response.Uri = Resolve(response.FrameworkId);
            return response;
        }

        /// <summary>
        /// Do we have frameworks?
        /// </summary>
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("frameworks")]
        [ExceptionHandling]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Head()
        {
            var frameworks = Get();
            if (frameworks != null && frameworks.Any())
            {
                return;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// framework exists?
        /// </summary>
        /// <param name="id">{FrameworkId}-{ProgType}-{PathwayId}</param>
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("frameworks/{id}")]
        [ExceptionHandling]
        public void Head(string id)
        {
            if (Get(id) != null)
            {
                return;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private string Resolve(string id)
        {
            return Url.Link("DefaultApi", new { controller = "frameworks", id = id });
        }
    }
}
