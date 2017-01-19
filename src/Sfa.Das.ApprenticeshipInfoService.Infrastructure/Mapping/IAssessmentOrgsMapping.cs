using Sfa.Das.ApprenticeshipInfoService.Application.Models;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
using Organisation = SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs.Organisation;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping
{
    public interface IAssessmentOrgsMapping
    {
        OrganisationSummary MapToOrganisationDto(Application.Models.Organisation organisation);

        Organisation MapToOrganisationDetailsDto(Application.Models.Organisation organisation);
    }
}