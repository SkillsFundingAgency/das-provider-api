using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;

namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    public class AngleSharpService : IAngleSharpService
    {
        public IList<string> GetLinks(string fromUrl, string selector, string textInTitle)
        {
            if (string.IsNullOrEmpty(fromUrl))
            {
                return new List<string>();
            }

            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString(fromUrl);

                try
                {
                    var parser = new HtmlParser();
                    var result = parser.Parse(htmlCode);
                    var all = result.QuerySelectorAll(selector);

                    return all.Where(x => x.InnerHtml.Contains(textInTitle)).Select(x => x.GetAttribute("href")).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
