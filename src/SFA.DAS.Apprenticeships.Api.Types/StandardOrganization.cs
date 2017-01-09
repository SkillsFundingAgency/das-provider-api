using System;

namespace SFA.DAS.Apprenticeships.Api.Types
{
    public class StandardOrganization
    {
        public string EpaOrganisationIdentifier { get; set; }

        public int StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }
    }
}
