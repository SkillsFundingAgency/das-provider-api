using System.Web.Mvc;
using System.Web.Routing;

namespace Sfa.Das.ApprenticeshipInfoService.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HealthRoute",
                url: "health/{action}/{id}",
                defaults: new { controller = "Health", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "HomeRoute",
                url: "home/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "DefaultRoute",
                url: string.Empty,
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}