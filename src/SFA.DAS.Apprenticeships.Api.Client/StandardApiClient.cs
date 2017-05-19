using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Apprenticeships.Api.Types;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public class StandardApiClient : ApiClientBase, IStandardApiClient
    {
        public StandardApiClient(string baseUri = null) : base(baseUri)
        {
        }

        public Standard Get(int standardCode)
        {
            return Get(standardCode.ToString());
        }

        public Task<Standard> GetAsync(int standardCode)
        {
            return GetAsync(standardCode.ToString());
        }

        public Standard Get(string standardCode)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/standards/{standardCode}"))
            {
                return RequestAndDeserialise<Standard>(request, $"Could not find the standard {standardCode}");
            }
        }

        public Task<Standard> GetAsync(string standardCode)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/standards/{standardCode}"))
            {
                return RequestAndDeserialiseAsync<Standard>(request, $"Could not find the standard {standardCode}");
            }
        }

        public bool Exists(int standardCode)
        {
            return Exists(standardCode.ToString());
        }

        public Task<bool> ExistsAsync(int standardCode)
        {
            return ExistsAsync(standardCode.ToString());
        }

        public bool Exists(string standardCode)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/standards/{standardCode}"))
            {
                return Exists(request);
            }
        }

        public Task<bool> ExistsAsync(string standardCode)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/standards/{standardCode}"))
            {
                return ExistsAsync(request);
            }
        }

        /// <summary>
        /// Get a collection of standards
        /// GET /standards
        /// </summary>
        /// <returns>a collection of standard summaries</returns>
        public IEnumerable<StandardSummary> FindAll()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/standards"))
            {
                return RequestAndDeserialise<IEnumerable<StandardSummary>>(request);
            }
        }

        public Task<IEnumerable<StandardSummary>> FindAllAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/standards"))
            {
                return RequestAndDeserialiseAsync<IEnumerable<StandardSummary>>(request);
            }
        }
    }
}