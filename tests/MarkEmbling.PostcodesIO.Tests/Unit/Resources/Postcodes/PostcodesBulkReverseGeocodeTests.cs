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

        [Test]
        public void BulkReverseGeocode_adds_json_body_to_request()
        {
            _postcodes.BulkReverseGeocode(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Count == 1 &&
                        req.Parameters[0].Type == ParameterType.RequestBody &&
                        req.Parameters[0].GetType() == typeof(JsonParameter))));
        }

        [Test]
        public async Task BulkReverseGeocodeAsync_adds_json_body_to_request()
        {
            await _postcodes.BulkReverseGeocodeAsync(new[] { new ReverseGeocodeQuery() });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Count == 1 &&
                        req.Parameters[0].Type == ParameterType.RequestBody &&
                        req.Parameters[0].GetType() == typeof(JsonParameter))));
        }

        [Test]
        public void BulkReverseLookup_request_body_has_expected_data_shape()
        {
            var lookupData = new[] { new ReverseGeocodeQuery() };
            _requestExecutorMock
                .Setup(x => x.ExecuteRequest<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(It.IsAny<RestRequest>()))
                .Callback<RestRequest>(restReq =>
                {
                    // Get the request body parameter and check that it has a postcodes property
                    // which contains the list of postcode strings lookupData.
                    var body = (JsonParameter)restReq.Parameters[0];
                    var bodyObj = body.Value;
                    Assert.AreEqual(
                        lookupData,
                        bodyObj.GetType().GetProperty("geolocations").GetValue(bodyObj));
                });

            _postcodes.BulkReverseGeocode(lookupData);

            _requestExecutorMock.VerifyAll();
        }

        [Test]
        public async Task BulkReverseGeocodeAsync_request_body_has_expected_data_shape()
        {
            var lookupData = new[] { new ReverseGeocodeQuery() };
            _requestExecutorMock
                .Setup(x => x.ExecuteRequestAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(It.IsAny<RestRequest>()))
                .Returns(Task.FromResult(default(List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>)))  // Required for callback to work for async method
                .Callback<RestRequest>(restReq =>
                {
                    // Get the request body parameter and check that it has a postcodes property
                    // which contains the list of postcode strings in lookupData.
                    var body = (JsonParameter)restReq.Parameters[0];
                    var bodyObj = body.Value;
                    Assert.AreEqual(
                        lookupData,
                        bodyObj.GetType().GetProperty("geolocations").GetValue(bodyObj));
                });

            await _postcodes.BulkReverseGeocodeAsync(lookupData);

            _requestExecutorMock.VerifyAll();
        }
    }
}
