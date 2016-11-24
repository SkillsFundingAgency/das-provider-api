namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public class HealthSettings : IHealthSettings
    {
        public string Environment => ConfigurationManager.AppSettings["Environment"];

        public IEnumerable<Uri> ElasticsearchUrls => GetElasticSearchIps("ElasticServerUrls");

        public string LarsSiteRootUrl => ConfigurationManager.AppSettings["LarsSiteRootUrl"];
        public string LarsSiteDownloadsPageUrl => ConfigurationManager.AppSettings["LarsSiteDownloadsPageUrl"];

        public string CourseDirectoryUrl => ConfigurationManager.AppSettings["CourseDirectoryUrl"];

        public string UkrlpUrl => ConfigurationManager.AppSettings["UkrlpUrl"];

        private IEnumerable<Uri> GetElasticSearchIps(string configString)
        {
            var urlStrings = ConfigurationManager.AppSettings[configString].Split(',');
            return urlStrings.Select(url => new Uri(url));
        }
    }
}