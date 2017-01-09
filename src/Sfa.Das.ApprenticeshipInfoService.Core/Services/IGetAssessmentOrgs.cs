namespace Sfa.Das.ApprenticeshipInfoService.Core.Services
{
    using System.Collections.Generic;
    using SFA.DAS.Apprenticeships.Api.Types;
    using SFA.DAS.Apprenticeships.Api.Types.DTOs;

    public interface IGetAssessmentOrgs
    {
        IEnumerable<OrganizationDTO> GetAllOrganizations();

        OrganizationDetailsDTO GetOrganizationById(string organizationId);

        IEnumerable<OrganizationDetailsDTO> GetOrganizationsByStandardId(string standardId);
    }
}
