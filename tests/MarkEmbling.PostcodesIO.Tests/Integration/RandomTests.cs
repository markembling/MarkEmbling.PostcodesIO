using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class RandomTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Random_returns_a_postcode_result() {
            var result = _client.Random();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Postcode));
        }

        [Test]
        public async Task Random_returns_a_postcode_result_async()
        {
            var result = await _client.RandomAsync();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Postcode));
        }
    }
}