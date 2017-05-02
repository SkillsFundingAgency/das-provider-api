namespace Sfa.Das.ApprenticeshipInfoService.UnitTests.Controllers
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using SFA.DAS.Providers.Api.Client;

    [TestFixture]
    public class ProvidersControllerApiClientTests
    {
        private ProviderApiClient _sut;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _sut = new ProviderApiClient("http://das-prd-apprenticeshipinfoservice.cloudapp.net");
        }

        [Test]
        [Ignore("Adhoc tests to verify live provider information")]
        public void ShouldAllProvidersHasContactInformation()
        {
            var result = _sut.FindAll().ToList();

            var emptyemail = result.Where(x => (string.IsNullOrWhiteSpace(x.Email) || string.IsNullOrEmpty(x.Email))).ToList();

            if (emptyemail != null)
            {
                Console.WriteLine($"There are {emptyemail.Count} providers without email :  {string.Join(",", emptyemail.Select(x => x.Ukprn))}");
            }

            var emptywebsite = result.Where(x => (string.IsNullOrWhiteSpace(x.Website) || string.IsNullOrEmpty(x.Website))).ToList();

            if (emptywebsite != null)
            {
                Console.WriteLine($"There are {emptywebsite.Count} providers without website :  {string.Join(",", emptywebsite.Select(x => x.Ukprn))}");
            }

            var emptyphone = result.Where(x => (string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrEmpty(x.Phone))).ToList();

            if (emptyphone != null)
            {
                Console.WriteLine($"There are {emptyphone.Count} providers without phone :  {string.Join(",", emptyphone.Select(x => x.Ukprn))}");
            }

            var emptyContactInfo = result.Where(x => (string.IsNullOrWhiteSpace(x.Email) || string.IsNullOrEmpty(x.Website) || string.IsNullOrEmpty(x.Phone)));
            Assert.IsTrue(emptyContactInfo == null, $"There are {emptyContactInfo.Count()} providers without contact info :  {string.Join(",", emptyContactInfo.Select(x => x.Ukprn))}");
        }
    }
}
