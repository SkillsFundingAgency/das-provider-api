using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Sfa.Das.ApprenticeshipInfoService.Core.Configuration;
using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Helpers;
using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Models;

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

        public async Task TrackApiCall(GaRouteTrackingArg gaRouteArgs)
        {
            try
            {
                var nvc = CreateQueryStringArgsForGaCollect(gaRouteArgs);
                var qs = UrlHelper.ToQueryString(nvc);

                var collectUrl = $"{GaCollectPrefixUrl}{qs}";

                _logger.Debug(collectUrl);

                var gaReq = WebRequest.Create(collectUrl);
                gaReq.Headers = new WebHeaderCollection { gaRouteArgs.OriginalRequestHeaders };

                await gaReq.GetResponseAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error with GA collection call.");
            }
        }

        private NameValueCollection CreateQueryStringArgsForGaCollect(GaRouteTrackingArg gaRouteArgs)
        {
            var nvc = new NameValueCollection
            {
                {"v", "1"}, // Version
                {"ds", "api"}, // Data Source
                {"t", "pageview"}, // Hit Type
                {"tid", _gaTrackingCode}, // Tracking ID
                {"cid", gaRouteArgs.ClientId}, // ClientID
                {"dh", gaRouteArgs.Host}, // Document Host Name
                {"dl", gaRouteArgs.AbsoluteUrl}, // Document Location
                {"ua", gaRouteArgs.UserAgent}, // User-Agent Override
                {"ul", "en-gb"}, // User Language
                {"an", gaRouteArgs.ApplicationName}, // Application Name
                {"av", gaRouteArgs.ApiVersion}, // Application Version
                {"dp", gaRouteArgs.Path}, // Document Path
                {"dt", $"{gaRouteArgs.CtrlName}-{gaRouteArgs.ActionName}"} // Document Title
            };

            if (!string.IsNullOrEmpty(gaRouteArgs.RemoteIp))
            {
                nvc.Add("uip", gaRouteArgs.RemoteIp); // IP Override
            }

            if (!string.IsNullOrEmpty(gaRouteArgs.UrlReferrer))
            {
                nvc.Add("dr", gaRouteArgs.UrlReferrer); // Document Referrer
            }

            return nvc;
        }
    }
}
