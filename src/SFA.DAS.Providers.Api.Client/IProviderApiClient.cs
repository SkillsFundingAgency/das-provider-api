using System;
using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public interface IProviderApiClient : IDisposable
    {
        /// <summary>
        /// Get a provider details
        /// GET /providers/{provider-ukprn}
        /// </summary>
        /// <param name="providerUkprn">an integer for the provider ukprn</param>
        /// <returns>a bool whether the provider exists</returns>
        Provider Get(long providerUkprn);

        /// <summary>
        /// Check if a provider exists
        /// HEAD /frameworks/{provider-ukprn}
        /// </summary>
        /// <param name="providerUkprn">an integer for the provider ukprn</param>
        /// <returns>bool</returns>
        bool Exists(long providerUkprn);

        /// <summary>
        /// Get all the active providers
        /// GET /providers/
        /// </summary>
        /// <returns>a collection of Providers</returns>
        IEnumerable<ProviderSummary> FindAll();
    }
}