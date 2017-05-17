using System.Web.Http.Description;
using SFA.DAS.Apprenticeships.Api.Types;

namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using Sfa.Das.ApprenticeshipInfoService.Api.Attributes;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using Swashbuckle.Swagger.Annotations;

    public class StandardsController : ApiController
    {
        private readonly IGetStandards _getStandards;

        public StandardsController(IGetStandards getStandards)
        {
            _getStandards = getStandards;
        }

        /// <summary>
        /// Get all the active standards
        /// </summary>
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<StandardSummary>))]
        [Route("standards")]
        [ExceptionHandling]
        public IEnumerable<StandardSummary> Get()
        {
            var response = _getStandards.GetAllStandards().ToList();

            foreach (var item in response)
            {
                item.Uri = Resolve(item.Id);
            }

            return response;
        }

        /// <summary>
        /// Get a standard
        /// </summary>
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Standard))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("standards/{id}")]
        [ExceptionHandling]
        public Standard Get(string id)
        {
            var standard = _getStandards.GetStandardById(id);

            if (standard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            standard.Uri = Resolve(standard.StandardId);
            return standard;
        }

        /// <summary>
        /// Do we have standards?
        /// </summary>
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("standards")]
        [ExceptionHandling]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Head()
        {
            Get();
        }

        /// <summary>
        /// Standard exists?
        /// </summary>
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("standards/{id}")]
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
            return Url.Link("DefaultApi", new { controller = "standards", id = id });
        }
    }
}
