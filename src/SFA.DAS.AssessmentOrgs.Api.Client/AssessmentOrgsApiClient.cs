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
        public Task<Organisation> GetAsync(string organisationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/{organisationId}"))
            {
                return RequestAndDeserialiseAsync<Organisation>(request, $"Could not find the organisation {organisationId}");
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
        public Task<IEnumerable<OrganisationSummary>> FindAllAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations"))
            {
                return RequestAndDeserialiseAsync<IEnumerable<OrganisationSummary>>(request);
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
        public Task<IEnumerable<Organisation>> ByStandardAsync(int standardId)
        {
            return ByStandardAsync(standardId.ToString());
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
        public Task<IEnumerable<Organisation>> ByStandardAsync(string standardId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/assessment-organisations/standards/{standardId}"))
            {
                return RequestAndDeserialiseAsync<IEnumerable<Organisation>>(request);
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
                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        return false;
                    }

                    RaiseResponseError(request, result);
                }

                return false;
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
                using (var response = _httpClient.SendAsync(request))
                {
                    var result = await response;
                    if (result.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        return false;
                    }

                    RaiseResponseError(request, result);
                }

                return false;
            }
        }
    }
}
