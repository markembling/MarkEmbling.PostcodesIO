using MarkEmbling.PostcodesIO.Data;
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

        private static void TestLookup_returns_populated_responseResult(NearestResult result)
        {
            Assert.That(result.Postcode, Is.Not.Null);
            Assert.That(result.Quality, Is.Not.Null);
            Assert.That(result.Eastings, Is.Not.Null);
            Assert.That(result.Northings, Is.Not.Null);
            Assert.That(result.Country, Is.Not.Null);
            Assert.That(result.NHSHealthAuthority, Is.Not.Null);
            Assert.That(result.Longitude, Is.Not.Null);
            Assert.That(result.ParliamentaryConstituency, Is.Not.Null);
            Assert.That(result.EuropeanElectoralRegion, Is.Not.Null);
            Assert.That(result.PrimaryCareTrust, Is.Not.Null);
            Assert.That(result.Region, Is.Not.Null);
            Assert.That(result.LSOA, Is.Not.Null);
            Assert.That(result.MSOA, Is.Not.Null);
            Assert.That(result.NUTS, Is.Not.Null);
            Assert.That(result.InCode, Is.Not.Null);
            Assert.That(result.OutCode, Is.Not.Null);
            Assert.That(result.AdminDistrict, Is.Not.Null);
            Assert.That(result.Parish, Is.Not.Null);
            Assert.That(result.AdminCounty, Is.Not.Null);
            Assert.That(result.AdminWard, Is.Not.Null);
            Assert.That(result.CED, Is.Not.Null);
            Assert.That(result.CCG, Is.Not.Null);
            Assert.That(result.Codes, Is.Not.Null);

            Assert.That(result.Distance, Is.Not.Null);
        }

        [Test]
        public void Nearest_returns_populated_response_limit()
        {
            var limit = 20;
            var results = _client.Nearest("GU1 1AA", limit);

            Assert.That(results.Count(), Is.EqualTo(limit));
        }

        [Test]
        public async Task Nearest_returns_populated_response_limit_async()
        {
            var limit = 20;
            var results = await _client.NearestAsync("GU1 1AA", limit);

            Assert.That(results.Count(), Is.EqualTo(limit));
        }

        //TODO: tests on radius, need to find postcode with other postcodes that are not near

    }
}