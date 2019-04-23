using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesNearestTests
    {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup()
        {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Nearest_returns_populated_response()
        {
            var results = _postcodes.Nearest("GU1 1AA");

            foreach (var result in results)
            {
                AssertPopulatedPostcodeResult(result);
            }
        }

        [Test]
        public async Task NearestAsync_returns_populated_response()
        {
            var results = await _postcodes.NearestAsync("GU1 1AA");

            foreach (var result in results)
            {
                AssertPopulatedPostcodeResult(result);
            }
        }

        [Test]
        public void Nearest_returns_populated_response_limit()
        {
            var limit = 20;
            var results = _postcodes.Nearest("GU1 1AA", limit);

            Assert.AreEqual(limit, results.Count());
        }

        [Test]
        public async Task NearestAsync_returns_populated_response_limit()
        {
            var limit = 20;
            var results = await _postcodes.NearestAsync("GU1 1AA", limit);

            Assert.AreEqual(limit, results.Count());
        }

        //TODO: tests on radius, need to find postcode with other postcodes that are not near

        private static void AssertPopulatedPostcodeResult(PostcodeResult result)
        {
            Assert.NotNull(result.Postcode);
            Assert.NotNull(result.Quality);
            Assert.NotNull(result.Eastings);
            Assert.NotNull(result.Northings);
            Assert.NotNull(result.Country);
            Assert.NotNull(result.NHSHealthAuthority);
            Assert.NotNull(result.Longitude);
            Assert.NotNull(result.ParliamentaryConstituency);
            Assert.NotNull(result.EuropeanElectoralRegion);
            Assert.NotNull(result.PrimaryCareTrust);
            Assert.NotNull(result.Region);
            Assert.NotNull(result.LSOA);
            Assert.NotNull(result.MSOA);
            Assert.NotNull(result.NUTS);
            Assert.NotNull(result.InCode);
            Assert.NotNull(result.OutCode);
            Assert.NotNull(result.AdminDistrict);
            Assert.NotNull(result.Parish);
            Assert.NotNull(result.AdminCounty);
            Assert.NotNull(result.AdminWard);
            Assert.NotNull(result.CCG);
            Assert.NotNull(result.Codes);
        }
    }
}