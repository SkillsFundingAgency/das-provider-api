using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace SFA.DAS.Providers.Api.Client
{
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
        public Provider Get(long providerUkprn)
        {
            return Get(providerUkprn.ToString());
        }

        public Task<Provider> GetAsync(long providerUkprn)
        {
            return GetAsync(providerUkprn.ToString());
        }

        public Provider Get(int providerUkprn)
        {
            return Get(providerUkprn.ToString());
        }

        public Task<Provider> GetAsync(int providerUkprn)
        {
            return GetAsync(providerUkprn.ToString());
        }

        public Provider Get(string providerUkprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/providers/{providerUkprn}"))
            {
                return RequestAndDeserialise<Provider>(request, $"The provider {providerUkprn} could not be found");
            }
        }

        public Task<Provider> GetAsync(string providerUkprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/providers/{providerUkprn}"))
            {
                return RequestAndDeserialiseAsync<Provider>(request, $"The provider {providerUkprn} could not be found");
            }
        }

        /// <summary>
        /// Check if a provider exists
        /// HEAD /frameworks/{provider-ukprn}
        /// </summary>
        /// <param name="providerUkprn">an integer for the provider ukprn</param>
        /// <returns>bool</returns>
        public bool Exists(long providerUkprn)
        {
            return Exists(providerUkprn.ToString());
        }

        public Task<bool> ExistsAsync(long providerUkprn)
        {
            return ExistsAsync(providerUkprn.ToString());
        }

        public bool Exists(int providerUkprn)
        {
            return Exists(providerUkprn.ToString());
        }

        public Task<bool> ExistsAsync(int providerUkprn)
        {
            return ExistsAsync(providerUkprn.ToString());
        }

        public bool Exists(string providerUkprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/providers/{providerUkprn}"))
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

                    RaiseResponseError("Unexpected exception", request, result);
                }

                return false;
            }
        }

        public async Task<bool> ExistsAsync(string providerUkprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/providers/{providerUkprn}"))
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

                    RaiseResponseError("Unexpected exception", request, result);
                }

                return false;
            }
        }

        public IEnumerable<ProviderSummary> FindAll()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/providers"))
            {
                return RequestAndDeserialise<IEnumerable<ProviderSummary>>(request);
            }
        }

        public Task<IEnumerable<ProviderSummary>> FindAllAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/providers"))
            {
                return RequestAndDeserialiseAsync<IEnumerable<ProviderSummary>>(request);
            }
        }
    }
}