using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public class AssessmentOrgsMapping : IAssessmentOrgsMapping
    {
        public OrganizationDTO MapToOrganizationDto(Organization organization)
        {
            return new OrganizationDTO
            {
                Id = organization.EpaOrganisationIdentifier,
                Name = organization.EpaOrganisation
            };
        }

        public OrganizationDetailsDTO MapToOrganizationDetailsDto(Organization organization)
        {
            if (organization == null)
            {
                return null;
            }

            return new OrganizationDetailsDTO
            {
                EpaOrganisationIdentifier = organization.EpaOrganisationIdentifier,
                EpaOrganisation = organization.EpaOrganisation,
                ContactAddress1 = organization.ContactAddress1,
                ContactAddress2 = organization.ContactAddress2,
                ContactAddress3 = organization.ContactAddress3,
                ContactAddress4 = organization.ContactAddress4,
                ContactPostcode = organization.ContactPostcode,
                OrganisationType = organization.OrganisationType,
                WebsiteLink = organization.WebsiteLink
            };
        }
    }
}
