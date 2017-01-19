using System;
using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public interface IAssessmentOrgsApiClient : IDisposable
    {
        /// <summary>
        /// Get a single organisation details
        /// GET /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>a organisation details based on id</returns>
        Organisation Get(string organisationId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organisation summaries</returns>
        IEnumerable<OrganisationSummary> FindAll();

        /// <summary>
        /// Check if a assessment organisation exists
        /// HEAD /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>bool</returns>
        bool Exists(string organisationId);
    }
}