using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class LookupLatLonTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void LookupLatLon_simple_query_returns_populated_response()
        {
            var results = _client.LookupLatLon(new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            }).ToList();

            TestLookupLatLon_simple_query_returns_populated_response(results);
        }

        [Test]
        public void LookupLatLon_with_limit_returns_only_that_number_of_results() {
            var results = _client.LookupLatLon(new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            }).ToList();

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public async Task LookupLatLon_simple_query_returns_populated_response_async()
        {
            var results = (await _client.LookupLatLonAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            })).ToList();

            TestLookupLatLon_simple_query_returns_populated_response(results);
        }

        [Test]
        public async Task LookupLatLon_with_limit_returns_only_that_number_of_results_async()
        {
            var results = (await _client.LookupLatLonAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            })).ToList();

            Assert.AreEqual(2, results.Count);
        }

        // TODO: tests for radius and wideSearch. Probably better as unit tests.
        private static void TestLookupLatLon_simple_query_returns_populated_response(List<PostcodeResult> results)
        {
            Assert.True(results.Any());
            //TODO probably a better way of writing this without using Assert.IsTrue
            Assert.IsTrue(results.Any(p => p.Postcode == "GU1 1AA"));
        }
    }
}