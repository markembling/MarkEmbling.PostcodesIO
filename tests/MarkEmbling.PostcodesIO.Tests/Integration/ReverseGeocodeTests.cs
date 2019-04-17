using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class ReverseGeocodeTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void ReverseGeocode_simple_query_returns_populated_response()
        {
            var results = _client.ReverseGeocode(new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            }).ToList();

            TestReverseGeocode_simple_query_returns_populated_response(results);
        }

        [Test]
        public void ReverseGeocode_with_limit_returns_only_that_number_of_results() {
            var results = _client.ReverseGeocode(new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            }).ToList();

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public async Task ReverseGeocode_simple_query_returns_populated_response_async()
        {
            var results = (await _client.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            })).ToList();

            TestReverseGeocode_simple_query_returns_populated_response(results);
        }

        [Test]
        public async Task ReverseGeocode_with_limit_returns_only_that_number_of_results_async()
        {
            var results = (await _client.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            })).ToList();

            Assert.AreEqual(2, results.Count);
        }

        // TODO: tests for radius and wideSearch. Probably better as unit tests.
        private static void TestReverseGeocode_simple_query_returns_populated_response(List<PostcodeResult> results)
        {
            Assert.True(results.Any());
            //TODO probably a better way of writing this without using Assert.IsTrue
            Assert.IsTrue(results.Any(p => p.Postcode == "GU1 1AA"));
        }


        [Test]
        public void OutwardCodeReverseGeocode_returns_populated_response()
        {
            var results = _client.OutwardCodeReverseGeocode(51.2430302947367, -0.564888615311003);
            Assert.True(results.Any());
        }

        [Test]
        public async Task OutwardCodeReverseGeocodeAsync_returns_populated_response()
        {
            var results = await _client.OutwardCodeReverseGeocodeAsync(51.2430302947367, -0.564888615311003);
            Assert.True(results.Any());
        }

        [Test]
        public void OutwardCodeReverseGeocode_with_limit_returns_limited_results()
        {
            var results = _client.OutwardCodeReverseGeocode(51.2430302947367, -0.564888615311003, limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task OutwardCodeReverseGeocodeAsync_with_limit_returns_limited_results()
        {
            var results = await _client.OutwardCodeReverseGeocodeAsync(51.2430302947367, -0.564888615311003, limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void OutwardCodeReverseGeocode_with_wide_radius_gives_more_results()
        {
            var results = _client.OutwardCodeReverseGeocode(51.2430302947367, -0.564888615311003, radius: 10000);
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public async Task OutwardCodeReverseGeocodeAsync_with_wide_radius_gives_more_results()
        {
            var results = await _client.OutwardCodeReverseGeocodeAsync(51.2430302947367, -0.564888615311003, radius: 10000);
            Assert.AreEqual(10, results.Count());
        }
    }
}