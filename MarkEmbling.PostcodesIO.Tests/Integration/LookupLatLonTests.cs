using System.Linq;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Integration {
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class LookupLatLonTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void LookupLatLon_simple_query_returns_populated_response() {
            var results = _client.LookupLatLon(new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            }).ToList();

            Assert.True(results.Any());
            Assert.AreEqual("GU1 1AA", results[0].Postcode);
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

        // TODO: tests for radius and wideSearch
    }
}