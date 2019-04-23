using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PlaceTests
    {
        private PlacesResource _client;

        [SetUp]
        public void Setup()
        {
            _client = new PlacesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void PlaceLookup_returns_a_populated_result()
        {
            var result = _client.Lookup("osgb4000000074564391");
            AssertPopulatedPlaceLookupResult(result);
        }

        [Test]
        public async Task PlaceLookupAsync_returns_a_populated_result()
        {
            var result = await _client.LookupAsync("osgb4000000074564391");
            AssertPopulatedPlaceLookupResult(result);
        }

        [Test]
        public void PlaceQuery_returns_matching_results()
        {
            var results = _client.Query("Kent");
            Assert.AreEqual(7, results.Count());
            Assert.AreEqual("Kent", results.First().Name1);
        }

        [Test]
        public async Task PlaceQueryAsync_returns_matching_results()
        {
            var results = await _client.QueryAsync("Kent");
            Assert.AreEqual(7, results.Count());
            Assert.AreEqual("Kent", results.First().Name1);
        }

        [Test]
        public void PlaceQuery_with_non_existent_place_returns_null()
        {
            var result = _client.Query("NONEXISTENT");
            Assert.IsNull(result);
        }

        [Test]
        public async Task PlaceQueryAsync_with_non_existent_place_returns_null()
        {
            var result = await _client.QueryAsync("NONEXISTENT");
            Assert.IsNull(result);
        }

        [Test]
        public void PlaceQuery_with_limit_returns_limited_results()
        {
            var results = _client.Query("Kent", 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task PlaceQueryAsync_with_limit_returns_limited_results()
        {
            var results = await _client.QueryAsync("Kent", 2);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void RandomPlace_returns_a_postcode_result()
        {
            var result = _client.Random();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Code));
        }

        [Test]
        public async Task RandomPlaceAsync_returns_a_postcode_result()
        {
            var result = await _client.RandomAsync();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Code));
        }

        private static void AssertPopulatedPlaceLookupResult(PlaceResult result)
        {
            Assert.AreEqual("osgb4000000074564391", result.Code);
            Assert.AreEqual("England", result.Country);
            Assert.AreEqual("Hampshire", result.CountyUnitary);
            Assert.AreEqual("County", result.CountyUnitaryType);
            Assert.AreEqual("New Forest", result.DistrictBorough);
            Assert.AreEqual("District", result.DistrictBoroughType);
            Assert.AreEqual(413940, result.Eastings);
            Assert.AreEqual(50.8926950373503, result.Latitude);
            Assert.AreEqual("Hamlet", result.LocalType);
            Assert.AreEqual(-1.80317162043943, result.Longitude);
            Assert.AreEqual(414169, result.MaxEastings);
            Assert.AreEqual(110616, result.MaxNorthings);
            Assert.AreEqual(413669, result.MinEastings);
            Assert.AreEqual(110116, result.MinNorthings);
            Assert.AreEqual("Kent", result.Name1);
            Assert.AreEqual(null, result.Name1Language);
            Assert.AreEqual(null, result.Name2);
            Assert.AreEqual(null, result.Name2Language);
            Assert.AreEqual(110375, result.Northings);
            Assert.AreEqual("South East", result.Region);
        }
    }
}
