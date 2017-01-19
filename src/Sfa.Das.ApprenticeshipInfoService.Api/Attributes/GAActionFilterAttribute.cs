namespace Sfa.Das.ApprenticeshipInfoService.Api.Attributes
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Mvc;
    using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Services;

    public class GaActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private readonly IAnalyticsService _analyticsService;

        public GaActionFilterAttribute()
        {
            _analyticsService = DependencyResolver.Current.GetService<IAnalyticsService>();
        }
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _analyticsService.TrackApiCall();

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}