using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Places
{
    [TestFixture]
    public class PlacesLookupTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private PlacesResource _places;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _places = new PlacesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void Lookup_request_method_is_get()
        {
            _places.Lookup("osgb4000000074564391");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<PlaceResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task LookupAsync_request_method_is_get()
        {
            await _places.LookupAsync("osgb4000000074564391");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<PlaceResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Lookup_calls_appropriate_resource_url_with_postcode()
        {
            _places.Lookup("osgb4000000074564391");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<PlaceResult>(
                    It.Is<RestRequest>(req => req.Resource == "places/osgb4000000074564391")));
        }

        [Test]
        public async Task LookupAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _places.LookupAsync("osgb4000000074564391");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<PlaceResult>(
                    It.Is<RestRequest>(req => req.Resource == "places/osgb4000000074564391")));
        }
    }
}
