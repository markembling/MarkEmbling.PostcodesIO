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
            Assert.That(result.Any(), Is.True);
            Assert.That(result.Contains("GU1 1AA"), Is.True);
        }

        [Test]
        public void Autocomplete_returns_null_for_uncompletable_postcode() {
            var result = _client.Autocomplete("X");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Autocomplete_limits_results_when_limit_is_given() {
            var result = _client.Autocomplete("GU1 1A", 2).ToList();
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Autocomplete_returns_full_postcodes_for_partial_async()
        {
            var result = (await _client.AutocompleteAsync("GU1 1A")).ToList();
            Assert.That(result.Any(), Is.True);
            Assert.That(result.Contains("GU1 1AA"), Is.True);
        }

        [Test]
        public async Task Autocomplete_returns_null_for_uncompletable_postcode_async()
        {
            var result = await _client.AutocompleteAsync("X");
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Autocomplete_limits_results_when_limit_is_given_async()
        {
            var result = (await _client.AutocompleteAsync("GU1 1A", 2)).ToList();
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}