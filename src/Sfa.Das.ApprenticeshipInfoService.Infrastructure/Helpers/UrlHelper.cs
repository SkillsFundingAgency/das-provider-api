using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Helpers
{
    public static class UrlHelper
    {
        public static string ToQueryString(IDictionary<string, string> dict)
        {
            var array = (
                            from kv in dict
                            where kv.Value != null
                            select $"{HttpUtility.UrlEncode(kv.Key)}={HttpUtility.UrlEncode(kv.Value)}"
                        ).ToArray();

            return $"?{string.Join("&", array)}";
        }
    }
}
