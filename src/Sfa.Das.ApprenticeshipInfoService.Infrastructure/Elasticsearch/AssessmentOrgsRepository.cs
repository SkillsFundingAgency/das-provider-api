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

        public OrganizationDetailsDTO GetOrganizationById(string organizationId)
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

        public IEnumerable<OrganizationDetailsDTO> GetOrganizationsByStandardId(string standardId)
        {
            var ids = GetOrganizationIdsByStandardId(standardId);

            return ids.Select(GetOrganizationById).ToList();
        }

        private IEnumerable<string> GetOrganizationIdsByStandardId(string standardId)
        {
            var take = GetOrganizationsAmountByStandardId(standardId);

            var results =
                _elasticsearchCustomClient.Search<StandardOrganization>(
                    s =>
                    s.Index(_applicationSettings.AssessmentOrgsIndexAlias)
                        .Type(Types.Parse("standardorganisationdocument"))
                        .From(0)
                        .Take(take)
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.StandardCode)
                                .Query(standardId))));

            return results.Documents.Select(result => result.EpaOrganisationIdentifier).ToList();
        }

        private int GetOrganizationsAmountByStandardId(string standardId)
        {
            var results =
                _elasticsearchCustomClient.Search<StandardOrganization>(
                    s =>
                    s.Index(_applicationSettings.AssessmentOrgsIndexAlias)
                        .Type(Types.Parse("standardorganisationdocument"))
                        .From(0)
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.StandardCode)
                                .Query(standardId))));
            return (int)results.HitsMetaData.Total;
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
