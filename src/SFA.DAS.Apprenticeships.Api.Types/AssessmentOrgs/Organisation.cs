﻿namespace SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs
{
    public class Organisation
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public Address Address { get; set; }
    }
}
