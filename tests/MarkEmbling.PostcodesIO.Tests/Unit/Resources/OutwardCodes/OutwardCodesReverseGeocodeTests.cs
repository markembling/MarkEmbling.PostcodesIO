using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.OutwardCodes
{
    [TestFixture]
    public class OutwardCodesReverseGeocodeTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _outcodes = new OutwardCodesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void ReverseGeocode_request_method_is_get()
        {
            _outcodes.ReverseGeocode(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task ReverseGeocodeAsync_request_method_is_get()
        {
            await _outcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void ReverseGeocode_calls_appropriate_resource_url()
        {
            _outcodes.ReverseGeocode(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes")));
        }

        [Test]
        public async Task ReverseGeocodeAsync_calls_appropriate_resource_url()
        {
            await _outcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery());
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes")));
        }

        [Test]
        public void ReverseGeocode_simple_query_has_lat_long_query_params()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            };

            _outcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
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

            await _outcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
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

            _outcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
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

            await _outcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
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

            _outcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
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

            await _outcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
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

            _outcodes.ReverseGeocode(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
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

            await _outcodes.ReverseGeocodeAsync(reverseGeocodeQuery);

            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req =>
                        req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == reverseGeocodeQuery.Radius)
                    )));
        }

        [Test]
        public void ReverseGeocode_query_with_WideSearch_throws_exception()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                WideSearch = true
            };

            Assert.Throws<InvalidOperationException>(
                () => _outcodes.ReverseGeocode(reverseGeocodeQuery));
        }

        [Test]
        public void ReverseGeocodeAsync_query_with_WideSearch_throws_exception()
        {
            var reverseGeocodeQuery = new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                WideSearch = true
            };

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _outcodes.ReverseGeocodeAsync(reverseGeocodeQuery));
        }
    }
}
