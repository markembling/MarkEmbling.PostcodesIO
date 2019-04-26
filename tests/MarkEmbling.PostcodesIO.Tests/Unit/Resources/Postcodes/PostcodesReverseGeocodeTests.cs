using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesReverseGeocodeTests
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
        public void ReverseGeocode_request_method_is_get()
        {
            _postcodes.ReverseGeocode(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task ReverseGeocodeAsync_request_method_is_get()
        {
            await _postcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void ReverseGeocode_calls_appropriate_resource()
        {
            _postcodes.ReverseGeocode(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        [Test]
        public async Task ReverseGeocodeAsync_calls_appropriate_resource()
        {
            await _postcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        [Test]
        public void ReverseGeocode_simple_query_has_lat_long_query_params()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            };

            _postcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => 
                        req.Parameters.Any(p => p.Name == "lon" && (double)p.Value == reverseGeocodeQuery.Longitude) &&
                        req.Parameters.Any(p => p.Name == "lat" && (double)p.Value == reverseGeocodeQuery.Latitude)
                    )));
        }

        [Test]
        public async Task ReverseGeocodeAsync_simple_query_has_lat_long_query_params()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            };

            await _postcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "lon" && (double)p.Value == reverseGeocodeQuery.Longitude) &&
                        req.Parameters.Any(p => p.Name == "lat" && (double)p.Value == reverseGeocodeQuery.Latitude)
                    )));
        }

        [Test]
        public void ReverseGeocode_simple_query_has_no_other_query_params()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            };

            _postcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 2)));
        }

        [Test]
        public async Task ReverseGeocodeAsync_simple_query_has_no_other_query_params()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            };

            await _postcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 2)));
        }

        [Test]
        public void ReverseGeocode_query_with_Limit_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 3
            };

            _postcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == reverseGeocodeQuery.Limit)
                    )));
        }

        [Test]
        public async Task ReverseGeocodeAsync_query_with_Limit_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 3
            };

            await _postcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == reverseGeocodeQuery.Limit)
                    )));
        }

        [Test]
        public void ReverseGeocode_query_with_Radius_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Radius = 500
            };

            _postcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == reverseGeocodeQuery.Radius)
                    )));
        }

        [Test]
        public async Task ReverseGeocodeAsync_query_with_Radius_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Radius = 500
            };

            await _postcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == reverseGeocodeQuery.Radius)
                    )));
        }

        [Test]
        public void ReverseGeocode_query_with_WideSearch_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                WideSearch = true
            };

            _postcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "wideSearch" && (bool)p.Value)
                    )));
        }

        [Test]
        public async Task ReverseGeocodeAsync_query_with_WideSearch_adds_appropriate_query_param()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                WideSearch = true
            };

            await _postcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "wideSearch" && (bool)p.Value)
                    )));
        }
    }
}
