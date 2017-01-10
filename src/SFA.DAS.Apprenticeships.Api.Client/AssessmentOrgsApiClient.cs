using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public class AssessmentOrgsApiClient : ApiClientBase, IAssessmentOrgsApiClient
    {
        public AssessmentOrgsApiClient(string baseUri = null) : base(baseUri)
        {
        }

        public OrganizationDetailsDTO Get(string organizationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/assessmentorgs/{organizationId}");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OrganizationDetailsDTO>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    RaiseResponseError($"Could not find the organization {organizationId}", request, result);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        public IEnumerable<OrganizationDTO> FindAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/assessmentorgs");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<OrganizationDTO>>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        public bool Exists(string organizationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"/assessmentorgs/{organizationId}");
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
