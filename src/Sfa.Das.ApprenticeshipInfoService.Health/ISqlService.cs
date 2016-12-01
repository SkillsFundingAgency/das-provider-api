using Sfa.Das.ApprenticeshipInfoService.Health.Models;

namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    public interface ISqlService
    {
        Status TestConnection(string connectionString);
    }
}