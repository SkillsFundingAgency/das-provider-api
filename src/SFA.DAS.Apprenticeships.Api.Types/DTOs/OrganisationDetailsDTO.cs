using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

namespace SFA.DAS.Apprenticeships.Api.Types.DTOs
{
    public class OrganisationDetailsDTO
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string EpaOrganisation { get; set; }

        public string Uri { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }

        public Address Address { get; set; }
    }
}
