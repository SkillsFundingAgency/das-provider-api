using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

namespace SFA.DAS.AssessmentOrgs.Api.Client
{
    public class AssessmentOrgsApiClient : ApiClientBase, IAssessmentOrgsApiClient
    {
        public AssessmentOrgsApiClient(string baseUri = null) : base(baseUri)
        {
        }

        /// <summary>
        /// Get a single organisation details
        /// GET /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>a organisation details based on id</returns>
        public Organisation Get(string organisationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/{organisationId}"))
            {
                return RequestAndDeserialise<Organisation>(request, $"Could not find the organisation {organisationId}");
            }
        }

        /// <summary>
        /// Get a single organisation details
        /// GET /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>a organisation details based on id</returns>
        public async Task<Organisation> GetAsync(string organisationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/{organisationId}"))
            {
                return await RequestAndDeserialiseAsync<Organisation>(request, $"Could not find the organisation {organisationId}");
            }
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organisation summaries</returns>
        public IEnumerable<OrganisationSummary> FindAll()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations"))
            {
                return RequestAndDeserialise<IEnumerable<OrganisationSummary>>(request);
            }
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organisation summaries</returns>
        public async Task<IEnumerable<OrganisationSummary>> FindAllAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations"))
            {
                return await RequestAndDeserialiseAsync<IEnumerable<OrganisationSummary>>(request);
            }
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        public IEnumerable<Organisation> ByStandard(int standardId)
        {
            return ByStandard(standardId.ToString());
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        public async Task<IEnumerable<Organisation>> ByStandardAsync(int standardId)
        {
            return await ByStandardAsync(standardId.ToString());
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        public IEnumerable<Organisation> ByStandard(string standardId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/standards/{standardId}"))
            {
                return RequestAndDeserialise<IEnumerable<Organisation>>(request);
            }
        }

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        public async Task<IEnumerable<Organisation>> ByStandardAsync(string standardId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/standards/{standardId}"))
            {
                return await RequestAndDeserialiseAsync<IEnumerable<Organisation>>(request);
            }
        }

        /// <summary>
        /// Check if a assessment organisation exists
        /// HEAD /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>bool</returns>
        public bool Exists(string organisationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/assessment-organisations/{organisationId}"))
            {
                return Exists(request);
            }
        }

        /// <summary>
        /// Check if a assessment organisation exists
        /// HEAD /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistsAsync(string organisationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/assessment-organisations/{organisationId}"))
            {
                return await ExistsAsync(request);
            }
        }
    }
}
