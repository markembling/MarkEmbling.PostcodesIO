using System.Linq;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Integration {
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class BulkLookupLatLonTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void BulkLookupLatLon_returns_results() {
            var lookups = new[] {
                new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613},
                new ReverseGeocodeQuery {Latitude = 51.2571984465953, Longitude = -0.567549033067429}
            };
            var results = _client.BulkLookupLatLon(lookups);

            Assert.AreEqual(2, results.Count());
        }
    }
}