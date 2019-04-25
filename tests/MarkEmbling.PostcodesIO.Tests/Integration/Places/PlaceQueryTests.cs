using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.Places
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PlaceQueryTests
    {
        private PlacesResource _client;

        [SetUp]
        public void Setup()
        {
            _client = new PlacesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Query_returns_matching_results()
        {
            var results = _client.Query("Kent");
            Assert.AreEqual(7, results.Count());
            Assert.AreEqual("Kent", results.First().Name1);
        }

        [Test]
        public async Task QueryAsync_returns_matching_results()
        {
            var results = await _client.QueryAsync("Kent");
            Assert.AreEqual(7, results.Count());
            Assert.AreEqual("Kent", results.First().Name1);
        }

        [Test]
        public void Query_with_non_existent_place_returns_empty()
        {
            var result = _client.Query("NONEXISTENT");
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task QueryAsync_with_non_existent_place_returns_empty()
        {
            var result = await _client.QueryAsync("NONEXISTENT");
            Assert.IsEmpty(result);
        }

        [Test]
        public void Query_with_limit_returns_limited_results()
        {
            var results = _client.Query("Kent", 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task QueryAsync_with_limit_returns_limited_results()
        {
            var results = await _client.QueryAsync("Kent", 2);
            Assert.AreEqual(2, results.Count());
        }
    }
}
