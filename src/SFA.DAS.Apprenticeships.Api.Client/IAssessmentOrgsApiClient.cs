using System;
using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public interface IAssessmentOrgsApiClient : IDisposable
    {
        /// <summary>
        /// Get a single organization details
        /// GET /assessmentorgs/{organizationId}
        /// </summary>
        /// <param name="organizationId">an integer for the organization id</param>
        /// <returns>a organization details based on id</returns>
        OrganizationDetailsDTO Get(string organizationId);

        /// <summary>
        /// Get a collection of organizations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organization summaries</returns>
        IEnumerable<OrganizationDTO> FindAll();

        /// <summary>
        /// Check if a assessment organization exists
        /// HEAD /assessmentorgs/{organizationId}
        /// </summary>
        /// <param name="organizationId">an integer for the organization id</param>
        /// <returns>bool</returns>
        bool Exists(string organizationId);
    }
}