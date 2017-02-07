using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Helpers
{
    public class UrlHelper
    {
        public static string ToQueryString(NameValueCollection nvc)
        {
            var array = (
                            from key in nvc.AllKeys
                            from value in nvc.GetValues(key)
                            select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}"
                        ).ToArray();

            return $"?{string.Join("&", array)}";
        }
    }
}
