using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public interface IAssessmentOrgsMapping
    {
        OrganizationDTO MapToOrganizationDto(Organization organization);

        OrganizationDetailsDTO MapToOrganizationDetailsDto(Organization organization);
    }
}