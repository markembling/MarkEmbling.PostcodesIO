using MarkEmbling.PostcodesIO.Data;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class BulkLookupLatLonTests {
        private PostcodesIOClient _client;
        private ReverseGeocodeQuery[] _lookups;
            
        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
            _lookups = [
                new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613},
                new ReverseGeocodeQuery {Latitude = 51.2571984465953, Longitude = -0.567549033067429}
            ];
        }

        [Test]
        public void BulkLookupLatLon_returns_results() {
            var results = _client.BulkLookupLatLon(_lookups);
            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Test]
        public void BulkLookupLatLon_results_contain_original_queries() {
            var results = _client.BulkLookupLatLon(_lookups).ToList();

            Assert.That(results.Any(x => x.Query.Equals(_lookups[0])), Is.True);
            Assert.That(results.Any(x => x.Query.Equals(_lookups[1])), Is.True);

        }

        [Test]
        public void BulkLookupLatLon_results_contain_postcode_results() {
            var results = _client.BulkLookupLatLon(_lookups).ToList();

            TestBulkLookupLatLon_results_contain_postcode_results(results);
        }

        [Test]
        public async Task BulkLookupLatLon_returns_results_async()
        {
            var results = await _client.BulkLookupLatLonAsync(_lookups);
            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task BulkLookupLatLon_results_contain_original_queries_async()
        {
            var results = (await _client.BulkLookupLatLonAsync(_lookups)).ToList();

            Assert.That(results.Any(x => x.Query.Equals(_lookups[0])), Is.True);
            Assert.That(results.Any(x => x.Query.Equals(_lookups[1])), Is.True);
        }

        [Test]
        public async Task BulkLookupLatLon_results_contain_postcode_results_async()
        {
            var results = (await _client.BulkLookupLatLonAsync(_lookups)).ToList();

            TestBulkLookupLatLon_results_contain_postcode_results(results);
        }

        private void TestBulkLookupLatLon_results_contain_postcode_results(List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeData>>> results)
        {
            Assert.That(results[0].Result.Any(), Is.True);
            Assert.That(results[1].Result.Any(), Is.True);
            Assert.That(results.Single(r => r.Query.Equals(_lookups[0])).Result.Exists(p => p.Postcode == "GU1 1AA"), Is.True);
        }
    }
}