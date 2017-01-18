﻿using System.Linq;
using Sfa.Das.ApprenticeshipInfoService.Core.Models;
using Sfa.Das.ApprenticeshipInfoService.Core.Models.Responses;
using SFA.DAS.Apprenticeships.Api.Types;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public class ProviderMapping : IProviderMapping
    {
        public ProviderDTO MapToProviderDto(Provider provider)
        {
            return new ProviderDTO
            {
                Email = provider.Email,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                IsEmployerProvider = provider.IsEmployerProvider,
                IsHigherEducationInstitute = provider.IsHigherEducationInstitute,
                LearnerSatisfaction = provider.LearnerSatisfaction,
                NationalProvider = provider.NationalProvider,
                Phone = provider.Phone,
                ProviderName = provider.ProviderName,
                Ukprn = provider.Ukprn,
                Uri = provider.Uri,
                Website = provider.Website
            };
        }

        ApprenticeshipDetails IProviderMapping.MapToProvider(StandardProviderSearchResultsItem item, int locationId)
        {
            if (item == null)
            {
                return null;
            }

            var details = MapFromInterface(item, locationId);
            details.Product.Apprenticeship.Code = item.StandardCode.ToString();

            return details;
        }

        public ApprenticeshipDetails MapToProvider(FrameworkProviderSearchResultsItem item, int locationId)
        {
            var details = MapFromInterface(item, locationId);

            details.Product.Apprenticeship.Code = item.FrameworkId;

            return details;
        }

        public StandardProviderSearchResultsItemResponse MapToStandardProviderResponse(
            StandardProviderSearchResultsItem item)
        {
            return new StandardProviderSearchResultsItemResponse
            {
                ProviderName = item.ProviderName,
                ApprenticeshipInfoUrl = item.ApprenticeshipInfoUrl,
                ApprenticeshipMarketingInfo = item.ApprenticeshipMarketingInfo,
                ContactUsUrl = item.ContactUsUrl,
                DeliveryModes = item.DeliveryModes,
                Distance = item.Distance,
                Email = item.Email,
                EmployerSatisfaction = item.EmployerSatisfaction,
                LearnerSatisfaction = item.LearnerSatisfaction,
                MarketingName = item.MarketingName,
                NationalOverallAchievementRate = item.NationalOverallAchievementRate,
                NationalProvider = item.NationalProvider,
                OverallAchievementRate = item.OverallAchievementRate,
                OverallCohort = item.OverallCohort,
                Phone = item.Phone,
                ProviderMarketingInfo = item.ProviderMarketingInfo,
                TrainingLocations = item.TrainingLocations,
                Ukprn = item.Ukprn,
                IsHigherEducationInstitute = item.IsHigherEducationInstitute,
                Website = item.Website
            };
        }

        public FrameworkProviderSearchResultsItemResponse MapToFrameworkProviderResponse(
            FrameworkProviderSearchResultsItem item)
        {
            return new FrameworkProviderSearchResultsItemResponse
            {
                ProviderName = item.ProviderName,
                ApprenticeshipInfoUrl = item.ApprenticeshipInfoUrl,
                ApprenticeshipMarketingInfo = item.ApprenticeshipMarketingInfo,
                ContactUsUrl = item.ContactUsUrl,
                DeliveryModes = item.DeliveryModes,
                Distance = item.Distance,
                Email = item.Email,
                EmployerSatisfaction = item.EmployerSatisfaction,
                LearnerSatisfaction = item.LearnerSatisfaction,
                MarketingName = item.MarketingName,
                NationalOverallAchievementRate = item.NationalOverallAchievementRate,
                NationalProvider = item.NationalProvider,
                OverallAchievementRate = item.OverallAchievementRate,
                OverallCohort = item.OverallCohort,
                Phone = item.Phone,
                ProviderMarketingInfo = item.ProviderMarketingInfo,
                TrainingLocations = item.TrainingLocations,
                Ukprn = item.Ukprn,
                IsHigherEducationInstitute = item.IsHigherEducationInstitute,
                Website = item.Website,
                FrameworkCode = item.FrameworkCode,
                Level = item.Level,
                PathwayCode = item.PathwayCode
            };
        }

        private static ApprenticeshipDetails MapFromInterface(IApprenticeshipProviderSearchResultsItem item, int locationId)
        {
            var matchingLocation = item.TrainingLocations.Single(x => x.LocationId == locationId);

            return new ApprenticeshipDetails
            {
                Product = new ApprenticeshipProduct
                {
                    Apprenticeship = new ApprenticeshipBasic
                    {
                        ApprenticeshipInfoUrl = item.ApprenticeshipInfoUrl,
                        ApprenticeshipMarketingInfo =
                                           item.ApprenticeshipMarketingInfo
                    },
                    DeliveryModes = item.DeliveryModes,
                    ProviderMarketingInfo = item.ProviderMarketingInfo,
                    EmployerSatisfaction = item.EmployerSatisfaction,
                    LearnerSatisfaction = item.LearnerSatisfaction,
                    AchievementRate = item.OverallAchievementRate,
                    NationalAchievementRate = item.NationalOverallAchievementRate,
                    OverallCohort = item.OverallCohort
                },
                Location = new Location
                {
                    LocationId = matchingLocation.LocationId,
                    LocationName = matchingLocation.LocationName,
                    Address = matchingLocation.Address
                },
                Provider = new ProviderDetail
                {
                    Name = item.ProviderName,
                    UkPrn = item.Ukprn,
                    IsHigherEducationInstitute = item.IsHigherEducationInstitute,
                    NationalProvider = item.NationalProvider,
                    ContactInformation = new ContactInformation
                    {
                        Phone = item.Phone,
                        Email = item.Email,
                        Website = item.Website,
                        ContactUsUrl = item.ContactUsUrl
                    },
                }
            };
        }
    }
}
