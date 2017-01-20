using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Apprenticeships.Api.Types;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public class FrameworkApiClient : ApiClientBase, IFrameworkApiClient
    {
        public FrameworkApiClient(string baseUri = null) : base(baseUri)
        {
        }

        public async Task<Framework> GetAsync(string frameworkId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/frameworks/{frameworkId}"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response;
                    if (result.Result.StatusCode == HttpStatusCode.OK)
                    {
                        await Task.Factory.StartNew(
                            () =>
                                JsonConvert.DeserializeObject<Framework>(result.Result.Content.ReadAsStringAsync().Result,
                                    _jsonSettings));
                    }
                    if (result.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        RaiseResponseError($"Could not find the framework {frameworkId}", request, result.Result);
                    }

                    RaiseResponseError(request, result.Result);
                }

                return null;
            }
        }

        public Framework Get(string frameworkId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/frameworks/{frameworkId}"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<Framework>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        RaiseResponseError($"Could not find the framework {frameworkId}", request, result);
                    }

                    RaiseResponseError(request, result);
                }

                return null;
            }
        }

        public Framework Get(int frameworkCode, int pathwayCode, int programmeType)
        {
            return Get(ConvertToCompositeId(frameworkCode, pathwayCode, programmeType)) ?? Get(ConvertToCompositeId2(frameworkCode, pathwayCode, programmeType));
        }

        public IEnumerable<FrameworkSummary> FindAll()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/frameworks"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<FrameworkSummary>>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }

                    RaiseResponseError(request, result);
                }

                return null;
            }
        }

        public bool Exists(string frameworkId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/frameworks/{frameworkId}"))
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


        public bool Exists(int frameworkCode, int pathwayCode, int progamType)
        {
            return Exists(ConvertToCompositeId(frameworkCode, pathwayCode, progamType));
        }

        private static string ConvertToCompositeId(int frameworkCode, int pathwayCode, int progamType)
        {
            return $"{frameworkCode}{progamType}{pathwayCode}";
        }
        private static string ConvertToCompositeId2(int frameworkCode, int pathwayCode, int progamType)
        {
            return $"{frameworkCode}-{progamType}-{pathwayCode}";
        }
    }
}
