using Sfa.Das.ApprenticeshipInfoService.Application.Models;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
using AppOrganisation = Sfa.Das.ApprenticeshipInfoService.Application.Models.Organisation;
using Organisation = SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs.Organisation;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public class AssessmentOrgsMapping : IAssessmentOrgsMapping
    {
        public OrganisationSummary MapToOrganisationDto(AppOrganisation organisation)
        {
            return new OrganisationSummary
            {
                Id = organisation.EpaOrganisationIdentifier,
                Name = organisation.EpaOrganisation
            };
        }

        public Organisation MapToOrganisationDetailsDto(AppOrganisation organisation)
        {
            if (organisation == null)
            {
                return null;
            }

            return new Organisation
            {
                EpaOrganisationIdentifier = organisation.EpaOrganisationIdentifier,
                EpaOrganisation = organisation.EpaOrganisation,
                Address = new Address
                {
                    Primary = organisation.Address.Primary,
                    Secondary = organisation.Address.Secondary,
                    Street = organisation.Address.Street,
                    Town = organisation.Address.Town,
                    Postcode = organisation.Address.Postcode
                },
                OrganisationType = organisation.OrganisationType,
                WebsiteLink = organisation.WebsiteLink
            };
        }
    }
}
