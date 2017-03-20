using System.Collections.Specialized;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Models
{
    public class GaRouteTrackingArg
    {
        public string ApplicationName { get; set; }
        public string CtrlName { get; set; }
        public string ActionName { get; set; }
        public string ApiVersion { get; set; }
        public string ClientId { get; set; }
        public string Host { get; set; }
        public string RawUrl { get; set; }
        public string AbsoluteUrl { get; set; }
        public string UrlReferrer { get; set; }
        public string RemoteIp { get; set; }
        public string UserAgent { get; set; }
        public string Path { get; set; }
        public NameValueCollection OriginalRequestHeaders { get; set; }
    }
}