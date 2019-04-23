using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesQueryTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Query_with_full_postcode_returns_matching_result() {
            var results = _postcodes.Query("GU1 1AA").ToList();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("GU1 1AA", results.First().Postcode);
        }

        [Test]
        public async Task QueryAsync_with_full_postcode_returns_matching_result()
        {
            var results = (await _postcodes.QueryAsync("GU1 1AA")).ToList();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("GU1 1AA", results.First().Postcode);
        }

        [Test]
        public void Query_with_partial_postcode_returns_all_matching_results() {
            var results = _postcodes.Query("GU1 1").ToList();
            Assert.True(results.Count() > 1);
        }

        [Test]
        public async Task QueryAsync_with_partial_postcode_returns_all_matching_results()
        {
            var results = (await _postcodes.QueryAsync("GU1 1")).ToList();
            Assert.True(results.Count() > 1);
        }

        [Test]
        public void Query_with_invalid_postcode_returns_nothing() {
            var result = _postcodes.Query("X");
            Assert.IsNull(result);
        }

        [Test]
        public async Task QueryAsync_with_invalid_postcode_returns_nothing()
        {
            var result = await _postcodes.QueryAsync("X");
            Assert.IsNull(result);
        }

        [Test]
        public void Query_with_limit_returns_limited_results() {
            var results = _postcodes.Query("GU1 1", 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task QueryAsync_with_limit_returns_limited_results()
        {
            var results = await _postcodes.QueryAsync("GU1 1", 2);
            Assert.AreEqual(2, results.Count());
        }
    }
}