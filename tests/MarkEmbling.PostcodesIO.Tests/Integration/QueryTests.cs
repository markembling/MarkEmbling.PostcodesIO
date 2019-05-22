using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class QueryTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Query_with_full_postcode_returns_matching_result() {
            var results = _client.Query("GU1 1AA").ToList();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("GU1 1AA", results.First().Postcode);
        }

        [Test]
        public void Query_with_partial_postcode_returns_all_matching_results() {
            var results = _client.Query("GU1 1").ToList();
            Assert.True(results.Count() > 1);
        }

        [Test]
        public void Query_with_invalid_postcode_returns_nothing() {
            var result = _client.Query("X");
            Assert.IsNull(result);
        }

        [Test]
        public void Query_with_limit_returns_limited_results() {
            var results = _client.Query("GU1 1", 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task Query_with_full_postcode_returns_matching_result_async()
        {
            var results = (await _client.QueryAsync("GU1 1AA")).ToList();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("GU1 1AA", results.First().Postcode);
        }

        [Test]
        public async Task Query_with_partial_postcode_returns_all_matching_results_async()
        {
            var results = (await _client.QueryAsync("GU1 1")).ToList();
            Assert.True(results.Count() > 1);
        }

        [Test]
        public async Task Query_with_invalid_postcode_returns_nothing_async()
        {
            var result = await _client.QueryAsync("X");
            Assert.IsNull(result);
        }

        [Test]
        public async Task Query_with_limit_returns_limited_results_async()
        {
            var results = await _client.QueryAsync("GU1 1", 2);
            Assert.AreEqual(2, results.Count());
        }
    }
}