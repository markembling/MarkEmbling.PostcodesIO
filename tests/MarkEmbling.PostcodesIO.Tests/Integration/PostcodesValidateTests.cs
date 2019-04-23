using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesValidateTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Validate_returns_true_for_valid_postcode() {
            var result = _postcodes.Validate("GU1 1AA");
            Assert.True(result);
        }

        [Test]
        public async Task ValidateAsync_returns_true_for_valid_postcode()
        {
            var result = await _postcodes.ValidateAsync("GU1 1AA");
            Assert.True(result);
        }

        [Test]
        public void Validate_returns_false_for_nonsense_postcode() {
            var result = _postcodes.Validate("FAKE_POSTCODE");
            Assert.False(result);
        }

        [Test]
        public async Task ValidateAsync_returns_false_for_nonsense_postcode()
        {
            var result = await _postcodes.ValidateAsync("FAKE_POSTCODE");
            Assert.False(result);
        }
    }
}