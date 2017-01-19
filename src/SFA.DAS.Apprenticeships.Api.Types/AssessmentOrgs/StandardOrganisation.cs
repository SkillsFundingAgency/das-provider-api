using System;

namespace SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs
{
    public class StandardOrganisation
    {
        public string EpaOrganisationIdentifier { get; set; }

        public int StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }
    }
}
