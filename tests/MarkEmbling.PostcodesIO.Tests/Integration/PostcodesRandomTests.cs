using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesRandomTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Random_returns_a_postcode_result() {
            var result = _postcodes.Random();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Postcode));
        }

        [Test]
        public async Task RandomAsync_returns_a_postcode_result()
        {
            var result = await _postcodes.RandomAsync();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Postcode));
        }
    }
}