using System.Collections.Generic;
using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
using Sfa.Das.ApprenticeshipInfoService.Core.Models.Responses;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Elasticsearch
{
    using System;
    using System.Linq;
    using Nest;
    using Sfa.Das.ApprenticeshipInfoService.Core.Configuration;
    using Sfa.Das.ApprenticeshipInfoService.Core.Models;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping;

    public sealed class AssessmentOrgsRepository : IGetAssessmentOrgs
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly ILog _applicationLogger;
        private readonly IConfigurationSettings _applicationSettings;
        private readonly IProviderLocationSearchProvider _providerLocationSearchProvider;
        private readonly IAssessmentOrgsMapping _assessmentOrgsMapping;

        public AssessmentOrgsRepository(
            IElasticsearchCustomClient elasticsearchCustomClient,
            ILog applicationLogger,
            IConfigurationSettings applicationSettings,
            IProviderLocationSearchProvider providerLocationSearchProvider, 
            IAssessmentOrgsMapping assessmentOrgsMapping)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _applicationLogger = applicationLogger;
            _applicationSettings = applicationSettings;
            _providerLocationSearchProvider = providerLocationSearchProvider;
            _assessmentOrgsMapping = assessmentOrgsMapping;
        }

        public IEnumerable<OrganizationDTO> GetAllOrganizations()
        {
            var take = GetOrganizationsTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<Organization>(
                    s =>
                    s.Index(_applicationSettings.AssessmentOrgsIndexAlias)
                        .Type(Types.Parse("organisationdocument"))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.EpaOrganisationIdentifier))
                        .Take(take)
                        .MatchAll());

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException($"Failed query all organizations");
            }

            return results.Documents.Select(organization => _assessmentOrgsMapping.MapToOrganizationDto(organization)).ToList();
        }

        public OrganizationDetailsDTO GetOrganizationsById(string organizationId)
        {
            var results =
                _elasticsearchCustomClient.Search<Organization>(
                    s =>
                    s.Index(_applicationSettings.AssessmentOrgsIndexAlias)
                        .Type(Types.Parse("organisationdocument"))
                        .From(0)
                        .Take(1)
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.EpaOrganisationIdentifier)
                                .Query(organizationId))));

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException($"Failed query organization by id");
            }

            return _assessmentOrgsMapping.MapToOrganizationDetailsDto(results.Documents.FirstOrDefault());
        }

        private int GetOrganizationsTotalAmount()
        {
            var results =
                _elasticsearchCustomClient.Search<Organization>(
                    s =>
                    s.Index(_applicationSettings.AssessmentOrgsIndexAlias)
                        .Type(Types.Parse("organisationdocument"))
                        .From(0)
                        .MatchAll());
            return (int)results.HitsMetaData.Total;
        }

        
    }
}
