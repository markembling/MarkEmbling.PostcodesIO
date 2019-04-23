using MarkEmbling.PostcodesIO.Internals;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class OutwardCodesReverseGeocodeTests
    {
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _outcodes = new OutwardCodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void ReverseGeocode_returns_populated_response()
        {
            var results = _outcodes.ReverseGeocode(51.2430302947367, -0.564888615311003);
            Assert.True(results.Any());
        }

        [Test]
        public async Task ReverseGeocodeAsync_returns_populated_response()
        {
            var results = await _outcodes.ReverseGeocodeAsync(51.2430302947367, -0.564888615311003);
            Assert.True(results.Any());
        }

        [Test]
        public void ReverseGeocode_with_limit_returns_limited_results()
        {
            var results = _outcodes.ReverseGeocode(51.2430302947367, -0.564888615311003, limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task ReverseGeocodeAsync_with_limit_returns_limited_results()
        {
            var results = await _outcodes.ReverseGeocodeAsync(51.2430302947367, -0.564888615311003, limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void ReverseGeocode_with_wide_radius_gives_more_results()
        {
            var results = _outcodes.ReverseGeocode(51.2430302947367, -0.564888615311003, radius: 10000);
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public async Task ReverseGeocodeAsync_with_wide_radius_gives_more_results()
        {
            var results = await _outcodes.ReverseGeocodeAsync(51.2430302947367, -0.564888615311003, radius: 10000);
            Assert.AreEqual(10, results.Count());
        }
    }
}