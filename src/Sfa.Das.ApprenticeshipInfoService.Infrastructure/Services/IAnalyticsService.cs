using System.Threading.Tasks;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Services
{
    public interface IAnalyticsService
    {
        Task TrackApiCall();
    }
}