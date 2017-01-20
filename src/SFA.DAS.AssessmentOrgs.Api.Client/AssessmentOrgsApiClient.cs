using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
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
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<Organisation>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        RaiseResponseError($"Could not find the organisation {organisationId}", request, result);
                    }

                    RaiseResponseError(request, result);

                }

                return null;
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
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return
                            JsonConvert.DeserializeObject<IEnumerable<OrganisationSummary>>(
                                result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }

                    RaiseResponseError(request, result);
                }

                return null;
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
                request.Headers.Add("Accept", "application/json");

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
    }
}
