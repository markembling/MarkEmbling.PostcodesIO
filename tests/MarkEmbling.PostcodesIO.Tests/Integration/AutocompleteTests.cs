using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class AutocompleteTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Autocomplete_returns_full_postcodes_for_partial() {
            var result = _client.Autocomplete("GU1 1A").ToList();
            Assert.True(result.Any());
            Assert.True(result.Contains("GU1 1AA"));
        }

        [Test]
        public void Autocomplete_returns_null_for_uncompletable_postcode() {
            var result = _client.Autocomplete("X");
            Assert.IsNull(result);
        }

        [Test]
        public void Autocomplete_limits_results_when_limit_is_given() {
            var result = _client.Autocomplete("GU1 1A", 2).ToList();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Autocomplete_returns_full_postcodes_for_partial_async()
        {
            var result = (await _client.AutocompleteAsync("GU1 1A")).ToList();
            Assert.True(result.Any());
            Assert.True(result.Contains("GU1 1AA"));
        }

        [Test]
        public async Task Autocomplete_returns_null_for_uncompletable_postcode_async()
        {
            var result = await _client.AutocompleteAsync("X");
            Assert.IsNull(result);
        }

        [Test]
        public async Task Autocomplete_limits_results_when_limit_is_given_async()
        {
            var result = (await _client.AutocompleteAsync("GU1 1A", 2)).ToList();
            Assert.AreEqual(2, result.Count());
        }
    }
}