using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.Places
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PlacesRandomTests
    {
        private PlacesResource _client;

        [SetUp]
        public void Setup()
        {
            _client = new PlacesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Random_returns_a_postcode_result()
        {
            var result = _client.Random();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Code));
        }

        [Test]
        public async Task RandomAsync_returns_a_postcode_result()
        {
            var result = await _client.RandomAsync();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Code));
        }
    }
}
