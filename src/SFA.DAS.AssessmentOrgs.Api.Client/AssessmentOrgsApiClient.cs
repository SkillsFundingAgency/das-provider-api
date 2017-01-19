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

        public Organisation Get(string organisationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/assessmentorgs/{organisationId}");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
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
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        public IEnumerable<OrganisationSummary> FindAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/assessmentorgs");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<OrganisationSummary>>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        public bool Exists(string organisationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"/assessmentorgs/{organisationId}");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
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
            finally
            {
                Dispose(request, response);
            }

            return false;
        }
    }
}
