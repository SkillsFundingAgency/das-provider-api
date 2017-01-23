namespace Sfa.Das.ApprenticeshipInfoService.UnitTests.Controllers
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using Sfa.Das.ApprenticeshipInfoService.Api.Controllers;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using FluentAssertions;
    using NUnit.Framework.Constraints;
    using Sfa.Das.ApprenticeshipInfoService.Core.Logging;
    using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

    public class AssessmentOrgsControllerTests
    {
        private AssessmentOrgsController _sut;
        private Mock<IGetAssessmentOrgs> _mockGetAssessmentOrgs;
        private Mock<ILog> _mockLogger;

        [SetUp]
        public void Init()
        {
            _mockGetAssessmentOrgs = new Mock<IGetAssessmentOrgs>();
            _mockLogger = new Mock<ILog>();

            _sut = new AssessmentOrgsController(
                _mockGetAssessmentOrgs.Object,
                _mockLogger.Object);
        }

        [Test]
        public void ShouldReturnAssessmentOrganisation()
        {
            var expected = new Organisation { Id = "EPA0001" };

            _sut.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/assessment-organisations")
            };
            _sut.Configuration = new HttpConfiguration();
            _sut.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            _sut.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "providers" } });

            _mockGetAssessmentOrgs.Setup(
                x =>
                    x.GetOrganisationById("EPA0001")).Returns(expected);

            var actual = _sut.Get("EPA0001");

            actual.ShouldBeEquivalentTo(expected);
            actual.Uri.Should().Be("http://localhost/assessment-organisations/EPA0001");
        }

        [Test]
        public void ShouldReturnAssessmentOrganisationNotFound()
        {
            var expected = new Organisation();

            _mockGetAssessmentOrgs.Setup(
                x =>
                    x.GetOrganisationById("EPA0001")).Returns(expected);

            ActualValueDelegate<object> test = () => _sut.Get("EPA0001x");

            Assert.That(test, Throws.TypeOf<HttpResponseException>());
        }

        [Test]
        public void ShouldReturnValidListOfAssessmentOrganisations()
        {
            var element = new OrganisationSummary { Id = "EPA0001" };
            var expected = new List<OrganisationSummary> { element };

            _sut.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/assessment-organisations")
            };
            _sut.Configuration = new HttpConfiguration();
            _sut.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            _sut.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "providers" } });


            _mockGetAssessmentOrgs.Setup(
                x =>
                    x.GetAllOrganisations()).Returns(expected);

            var response = _sut.Get();

            response.Should().NotBeNull();
            response.Should().BeOfType<List<OrganisationSummary>>();
            response.Should().NotBeEmpty();
            response.Should().BeEquivalentTo(expected);
            response.First().Should().NotBe(null);
            response.First().Should().Be(element);
            response.First().Id.Should().Be(element.Id);
            response.First().Uri.Should().Be("http://localhost/assessment-organisations/EPA0001");
        }
    }
}