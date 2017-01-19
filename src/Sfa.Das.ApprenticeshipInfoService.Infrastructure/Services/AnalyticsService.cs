using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Sfa.Das.ApprenticeshipInfoService.Core.Configuration;
using Sfa.Das.ApprenticeshipInfoService.Core.Logging;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ILog _logger;
        private const string GaCollectPrefixUrl = "https://www.google-analytics.com/collect";
        private readonly string _gaTrackingCode;

        public AnalyticsService(ILog logger, IConfigurationSettings settings)
        {
            _logger = logger;
            _gaTrackingCode = settings.GaTrackingCode;
        }

        public async Task TrackApiCall()
        {
            try
            {
                var nvc = CreateQueryStringForGaCollect();
            
                var collectUrl = $"{GaCollectPrefixUrl}{ToQueryString(nvc)}";
                _logger.Debug(collectUrl);
            
                var gaReq = WebRequest.Create(collectUrl);
                await gaReq.GetResponseAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error with GA collection call.");
            }
        }

        private NameValueCollection CreateQueryStringForGaCollect()
        {
            var incomingReq = HttpContext.Current.Request;

            return new NameValueCollection
            {
                {"v", "1"},
                {"da", "api"}, // Data Source
                {"t", "pageview"}, // Type
                {"tid", _gaTrackingCode},
                {"cid", "b87aa228-6bdb-44fd-ac5c-9acde7e0ba4c"}, // ClientID    // could be namespace of client sent in origin header
                {"dh", incomingReq.RawUrl},
                {"uip", incomingReq.ServerVariables["REMOTE_ADDR"]}, // IP Override
                {"ua", incomingReq.UserAgent}, // User-Agent Override
                {"dp", "frameworks"},
                {"dt", "Frameworks"}
            };
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (
                            from key in nvc.AllKeys
                            from value in nvc.GetValues(key)
                            where value != null
                            select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}"
                        ).ToArray();

            return $"?{string.Join("&", array)}";
        }
    }
}
