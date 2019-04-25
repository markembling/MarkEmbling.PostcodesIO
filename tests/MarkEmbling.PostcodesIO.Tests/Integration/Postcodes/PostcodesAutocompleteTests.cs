using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.Postcodes
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesAutocompleteTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Autocomplete_returns_full_postcodes_for_partial() {
            var result = _postcodes.Autocomplete("GU1 1A").ToList();
            Assert.True(result.Any());
            Assert.True(result.Contains("GU1 1AA"));
        }

        [Test]
        public async Task AutocompleteAsync_returns_full_postcodes_for_partial()
        {
            var result = (await _postcodes.AutocompleteAsync("GU1 1A")).ToList();
            Assert.True(result.Any());
            Assert.True(result.Contains("GU1 1AA"));
        }

        [Test]
        public void Autocomplete_returns_empty_for_uncompletable_postcode() {
            var result = _postcodes.Autocomplete("X");
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task AutocompleteAsync_returns_empty_for_uncompletable_postcode()
        {
            var result = await _postcodes.AutocompleteAsync("X");
            Assert.IsEmpty(result);
        }

        [Test]
        public void Autocomplete_limits_results_when_limit_is_given() {
            var result = _postcodes.Autocomplete("GU1 1A", 2).ToList();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task AutocompleteAsync_limits_results_when_limit_is_given()
        {
            var result = (await _postcodes.AutocompleteAsync("GU1 1A", 2)).ToList();
            Assert.AreEqual(2, result.Count());
        }
    }
}