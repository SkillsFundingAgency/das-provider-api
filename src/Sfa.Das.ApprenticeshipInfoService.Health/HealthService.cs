using System.Linq;

namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Elasticsearch;

    using Models;

    public class HealthService : IHealthService
    {
        private readonly IElasticsearchHealthService _elasticsearchHealthService;
        private readonly IAngleSharpService _angleSharpService;

        private readonly IHealthSettings _healthSettings;

        private readonly IHttpServer _httpServer;

        public HealthService(
            IElasticsearchHealthService elasticsearchHealthService,
            IAngleSharpService angleSharpService,
            IHealthSettings healthSettings,
            IHttpServer httpServer)
        {
            _elasticsearchHealthService = elasticsearchHealthService;
            _angleSharpService = angleSharpService;
            _healthSettings = healthSettings;
            _httpServer = httpServer;
        }

        public HealthModel CreateModel()
        {
            var timer = Stopwatch.StartNew();
            var elasticsearchModel = _elasticsearchHealthService.GetElasticHealth(_healthSettings.ElasticsearchUrls, _healthSettings.Environment);
            var elasticErrorLogs = _elasticsearchHealthService.GetErrorLogs(
                _healthSettings.ElasticsearchUrls,
                _healthSettings.Environment);

            var larsDownloadPageUrl = string.Concat(_healthSettings.LarsSiteRootUrl,
                _healthSettings.LarsSiteDownloadsPageUrl);

            var larsDownloadPageStatus = _httpServer.ResponseCode(larsDownloadPageUrl);
            var courseDirectoryResponse = _httpServer.ResponseCode(_healthSettings.CourseDirectoryUrl);

            var model = new HealthModel
            {
                Status = Status.Green,
                Errors = new List<string>(),
                ElasticSearchAliases = elasticsearchModel.ElasticsearchAliases,
                ElasticsearchLog = elasticErrorLogs,
                LarsFilePageStatus = larsDownloadPageStatus,
                CourseDirectoryStatus = courseDirectoryResponse 
            };

            if (elasticsearchModel.Exception != null)
            {
                model.Status = Status.Red;
                model.Errors.Add(elasticsearchModel.Exception.Message);
            }

            if (elasticsearchModel.ElasticsearchAliases.Count < 2)
            {
                model.Status = Status.Red;
                model.Errors.Add($"Missing aliases / indices. Should be 2 but found {elasticsearchModel.ElasticsearchAliases.Count}");
            }

            if (model.LarsFilePageStatus != Status.Green)
            {
                model.Status = Status.Red;
                model.Errors.Add("Cant access hub.imservices.org.uk (LARS)");
            }
            else
            {
                var links = _angleSharpService.GetLinks(larsDownloadPageUrl, "li a", "LARS CSV");
                var linkEndpoint = links?.FirstOrDefault();
                model.LarsZipFileStatus = _httpServer.ResponseCode(string.Concat(_healthSettings.LarsSiteRootUrl, linkEndpoint));
            }

            if (model.CourseDirectoryStatus != Status.Green)
            {
                model.Status = Status.Red;
                model.Errors.Add("Cant access Course Directory");
            }

            timer.Stop();
            model.Took = timer.ElapsedMilliseconds;

            return model;
        }
    }
}
