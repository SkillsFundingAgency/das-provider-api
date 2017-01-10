using System;
using SFA.DAS.Apprenticeships.Api.Types;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Newtonsoft.Json;

    public class ProviderApiClient : ApiClientBase, IProviderApiClient
    {
        public ProviderApiClient(string baseUri = null) : base(baseUri)
        {
        }

        /// <summary>
        /// Get a provider details
        /// GET /providers/{provider-ukprn}
        /// </summary>
        /// <param name="providerUkprn">an integer for the provider ukprn</param>
        /// <returns>a provider details based on ukprn</returns>
        public IEnumerable<Provider> Get(int providerUkprn)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/providers/{providerUkprn}");
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        var single = JsonConvert.DeserializeObject<Provider>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                        return new List<Provider> { single };
                    }
                    catch
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Provider>>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }
                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    RaiseResponseError($"The provider {providerUkprn} could not be found", request, result);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        /// <summary>
        /// Check if a provider exists
        /// HEAD /frameworks/{provider-ukprn}
        /// </summary>
        /// <param name="providerUkprn">an integer for the provider ukprn</param>
        /// <returns>bool</returns>
        public bool Exists(int providerUkprn)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"/providers/{providerUkprn}");
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

                RaiseResponseError("Unexpected exception", request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return false;
        }
    }
}