using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PlaceTests
    {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void PlaceLookup_returns_a_populated_result()
        {
            var result = _client.PlaceLookup("osgb4000000074564391");
            AssertPlaceLookupResult(result);
        }

        [Test]
        public async Task PlaceLookupAsync_returns_a_populated_result()
        {
            var result = await _client.PlaceLookupAsync("osgb4000000074564391");
            AssertPlaceLookupResult(result);
        }

        private static void AssertPlaceLookupResult(PlaceResult result)
        {
            Assert.AreEqual("osgb4000000074564391", result.Code);
            Assert.AreEqual("England", result.Country);
            Assert.AreEqual("Hampshire", result.CountryUnitary);
            Assert.AreEqual("County", result.CountryUnitaryType);
            Assert.AreEqual("New Forest", result.DistrictBorough);
            Assert.AreEqual("District", result.DistrictBoroughType);
            Assert.AreEqual(413940, result.Eastings);
            Assert.AreEqual(50.8926950373503, result.Latituide);
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
