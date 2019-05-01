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
    public class PlacesRandomTests
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
        public void Random_request_method_is_get()
        {
            _places.Random();
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<PlaceResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task RandomAsync_request_method_is_get()
        {
            await _places.RandomAsync();
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<PlaceResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Random_calls_appropriate_resource_url()
        {
            _places.Random();
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<PlaceResult>(
                    It.Is<RestRequest>(req => req.Resource == "random/places")));
        }

        [Test]
        public async Task RandomAsync_calls_appropriate_resource_url()
        {
            await _places.RandomAsync();
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<PlaceResult>(
                    It.Is<RestRequest>(req => req.Resource == "random/places")));
        }
    }
}
