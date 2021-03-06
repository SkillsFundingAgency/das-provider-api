﻿namespace Sfa.Das.ApprenticeshipInfoService.Health.DependencyResolution
{
    using Sfa.Das.ApprenticeshipInfoService.Health.Elasticsearch;

    using StructureMap;

    public class HealthRegistry : Registry
    {
        public HealthRegistry()
        {
            For<IHealthSettings>().Use<HealthSettings>();
            For<IElasticsearchHealthService>().Use<ElasticsearchHealthHealthService>();
            For<IAngleSharpService>().Use<AngleSharpService>();
            For<IHttpServer>().Use<HttpService>();
            For<IHealthService>().Use<HealthService>();
            For<ISqlService>().Use<SqlService>();
        }
    }
}
