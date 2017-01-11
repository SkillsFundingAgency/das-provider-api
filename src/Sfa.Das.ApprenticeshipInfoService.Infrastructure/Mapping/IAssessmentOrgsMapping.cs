using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.DTOs;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public interface IAssessmentOrgsMapping
    {
        OrganisationDTO MapToOrganisationDto(Organisation organisation);

        OrganisationDetailsDTO MapToOrganisationDetailsDto(Organisation organisation);
    }
}