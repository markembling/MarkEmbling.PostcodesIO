using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class NearestTests
    {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Nearest_returns_populated_response()
        {
            var results = _client.Nearest("GU1 1AA");

            foreach (var result in results)
            {
                TestLookup_returns_populated_responseResult(result);
            }
        }

        [Test]
        public async Task Nearest_returns_populated_response_async()
        {
            var results = await _client.NearestAsync("GU1 1AA");

            foreach (var result in results)
            {
                TestLookup_returns_populated_responseResult(result);
            }
        }

        private static void TestLookup_returns_populated_responseResult(PostcodeResult result)
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

        [Test]
        public void Nearest_returns_populated_response_limit()
        {
            var limit = 20;
            var results = _client.Nearest("GU1 1AA", limit);

            Assert.AreEqual(limit, results.Count());
        }

        [Test]
        public async Task Nearest_returns_populated_response_limit_async()
        {
            var limit = 20;
            var results = await _client.NearestAsync("GU1 1AA", limit);

            Assert.AreEqual(limit, results.Count());
        }

        //TODO: tests on radius, need to find postcode with other postcodes that are not near

        [Test]
        public void NearestOutwardCode_returns_populated_response()
        {
            var results = _client.NearestOutwardCode("GU1");
            Assert.AreEqual(4, results.Count());
        }

        [Test]
        public async Task NearestOutwardCodeAsync_returns_populated_response()
        {
            var results = await _client.NearestOutwardCodeAsync("GU1");

            Assert.AreEqual(4, results.Count());
            foreach (var result in results)
            {
                AssertOutwardCodeResultIsPopulated(result);
            }
        }

        [Test]
        public void NearestOutwardCode_with_limit_returns_limited_results()
        {
            var results = _client.NearestOutwardCode("GU1", limit: 2);

            Assert.AreEqual(2, results.Count());
            foreach (var result in results)
            {
                AssertOutwardCodeResultIsPopulated(result);
            }
        }

        [Test]
        public async Task NearestOutwardCodeAsync_with_limit_returns_limited_results()
        {
            var results = await _client.NearestOutwardCodeAsync("GU1", limit: 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void NearestOutwardCode_with_wide_radius_gives_more_results()
        {
            var results = _client.NearestOutwardCode("GU1", radius: 10000);
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public async Task NearestOutwardCodeAsync_with_wide_radius_gives_more_results()
        {
            var results = await _client.NearestOutwardCodeAsync("GU1", radius: 10000);
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