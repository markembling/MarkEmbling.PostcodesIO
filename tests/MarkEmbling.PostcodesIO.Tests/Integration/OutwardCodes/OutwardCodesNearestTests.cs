using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.OutwardCodes
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class OutwardCodesNearestTests
    {
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _outcodes = new OutwardCodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Nearest_returns_populated_response()
        {
            var results = _outcodes.Nearest("GU1");
            Assert.AreEqual(4, results.Count());
        }

        [Test]
        public async Task NearestAsync_returns_populated_response()
        {
            var results = await _outcodes.NearestAsync("GU1");

            Assert.AreEqual(4, results.Count());
            foreach (var result in results)
            {
                AssertOutwardCodeResultIsPopulated(result);
            }
        }

        [Test]
        public void Nearest_with_limit_returns_limited_results()
        {
            var results = _outcodes.Nearest("GU1", limit: 2);

            Assert.AreEqual(2, results.Count());
            foreach (var result in results)
            {
                AssertOutwardCodeResultIsPopulated(result);
            }
        }

        [Test]
        public async Task NearestAsync_with_limit_returns_limited_results()
        {
            var results = await _outcodes.NearestAsync("GU1", limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void Nearest_with_wide_radius_gives_more_results()
        {
            var results = _outcodes.Nearest("GU1", radius: 10000);
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public async Task NearestAsync_with_wide_radius_gives_more_results()
        {
            var results = await _outcodes.NearestAsync("GU1", radius: 10000);
            Assert.AreEqual(10, results.Count());
        }

        private static void AssertOutwardCodeResultIsPopulated(OutwardCodeResult result)
        {
            Assert.NotNull(result.AdminCounty);
            Assert.NotNull(result.AdminDistrict);
            Assert.NotNull(result.AdminWard);
            Assert.NotNull(result.Country);
            Assert.NotNull(result.Eastings);
            Assert.NotNull(result.Latitude);
            Assert.NotNull(result.Longitude);
            Assert.NotNull(result.Northings);
            Assert.NotNull(result.Outcode);
            Assert.NotNull(result.Parish);
        }
    }
}