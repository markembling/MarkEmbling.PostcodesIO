using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesBulkReverseGeocodeTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _postcodes = new PostcodesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void BulkReverseGeocode_request_method_is_post()
        {
            _postcodes.BulkReverseGeocode(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req => req.Method == Method.POST)));
        }

        [Test]
        public async Task BulkReverseGeocodeAsync_request_method_is_post()
        {
            await _postcodes.BulkReverseGeocodeAsync(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req => req.Method == Method.POST)));
        }

        [Test]
        public void BulkReverseGeocode_calls_appropriate_resource_url()
        {
            _postcodes.BulkReverseGeocode(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        [Test]
        public async Task BulkReverseGeocodeAsync_calls_appropriate_resource_url()
        {
            await _postcodes.BulkReverseGeocodeAsync(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        // TODO: test that body is set appropriately
        // TODO: test JSON serialisation of ReverseGeocodeQuery (somewhere else)
    }
}
