using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Integration {
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class BulkLookupLatLonTests {
        private PostcodesIOClient _client;
        private ReverseGeocodeQuery[] _lookups;
            
        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
            _lookups = new[] {
                new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613},
                new ReverseGeocodeQuery {Latitude = 51.2571984465953, Longitude = -0.567549033067429}
            };
        }

        [Test]
        public void BulkLookupLatLon_returns_results() {
            var results = _client.BulkLookupLatLon(_lookups);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void BulkLookupLatLon_results_contain_original_queries() {
            var results = _client.BulkLookupLatLon(_lookups).ToList();

            Assert.True(results.Any(x => x.Query.Equals(_lookups[0])));
            Assert.True(results.Any(x => x.Query.Equals(_lookups[1])));
        }

        [Test]
        public void BulkLookupLatLon_results_contain_postcode_results() {
            var results = _client.BulkLookupLatLon(_lookups).ToList();

            Assert.True(results[0].Result.Any());
            Assert.True(results[1].Result.Any());
            Assert.True(results.Single(r => r.Query.Equals(_lookups[0])).Result.ElementAt(0).Postcode == "GU1 1AA");
        }
    }
}