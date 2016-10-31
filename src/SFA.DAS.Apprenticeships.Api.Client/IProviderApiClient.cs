using System;
using SFA.DAS.Apprenticeships.Api.Types;

namespace SFA.DAS.Apprenticeships.Api.Client
{
    public interface IProviderApiClient : IDisposable
    {
        Provider Get(int providerUkprn);
    }
}