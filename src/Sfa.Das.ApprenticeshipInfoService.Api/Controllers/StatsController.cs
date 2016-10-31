using System.Web.Http.Description;

namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Http;
    using SFA.DAS.Apprenticeships.Api.Types.Stats;

    public class StatsController : ApiController
    {
        [Route("stats/version")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public VersionInformation Version()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string assemblyInformationalVersion = fileVersionInfo.ProductVersion;
            return new VersionInformation
            {
                BuildId = ConfigurationManager.AppSettings["BuildId"],
                Version = assemblyInformationalVersion,
                AssemblyVersion = version
            };
        }
    }
}
