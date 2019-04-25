using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.Places
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PlaceLookupTests
    {
        private PlacesResource _client;

        [SetUp]
        public void Setup()
        {
            _client = new PlacesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Lookup_returns_a_populated_result()
        {
            var result = _client.Lookup("osgb4000000074564391");
            AssertPopulatedPlaceLookupResult(result);
        }

        [Test]
        public async Task LookupAsync_returns_a_populated_result()
        {
            var result = await _client.LookupAsync("osgb4000000074564391");
            AssertPopulatedPlaceLookupResult(result);
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
