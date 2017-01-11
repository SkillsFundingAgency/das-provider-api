using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public class AssessmentOrgsMapping : IAssessmentOrgsMapping
    {
        public OrganisationDTO MapToOrganisationDto(Organisation organisation)
        {
            return new OrganisationDTO
            {
                Id = organisation.EpaOrganisationIdentifier,
                Name = organisation.EpaOrganisation
            };
        }

        public OrganisationDetailsDTO MapToOrganisationDetailsDto(Organisation organisation)
        {
            if (organisation == null)
            {
                return null;
            }

            return new OrganisationDetailsDTO
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
