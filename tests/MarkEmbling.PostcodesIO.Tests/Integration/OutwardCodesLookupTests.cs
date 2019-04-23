using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class OutwardCodesLookupTests
    {
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _outcodes = new OutwardCodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Lookup_returns_populated_response()
        {
            var result = _outcodes.Lookup("IP3");

            AssertPopulatedOutwardCodeResult(result);
        }

        [Test]
        public async Task LookupAsync_returns_populated_response()
        {
            var result = await _outcodes.LookupAsync("IP3");
            AssertPopulatedOutwardCodeResult(result);
        }

        [Test]
        public void Null_longitude_and_latitude_in_lookup_are_handled()
        {
            var result = _outcodes.Lookup("JE2");

            Assert.IsNull(result.Longitude);
            Assert.IsNull(result.Latitude);
        }

        private static void AssertPopulatedOutwardCodeResult(OutwardCodeResult result)
        {
            Assert.AreEqual("IP3", result.Outcode);
            Assert.AreEqual(1.18785462894737, result.Longitude);
            Assert.AreEqual(52.0414095157894, result.Latitude);
            Assert.AreEqual(242908, result.Northings);
            Assert.AreEqual(618721, result.Eastings);
            Assert.AreEqual(
                new List<string>() {
                    "Suffolk Coastal",
                    "Ipswich"
                },
                result.AdminDistrict);
            Assert.AreEqual(
                new List<string>() {
                    "Ipswich, unparished area",
                    "Rushmere St. Andrew",
                    "Purdis Farm"
                },
                result.Parish);
            Assert.AreEqual(new List<string>() { "Suffolk" }, result.AdminCounty);
            Assert.AreEqual(
                new List<string>() {
                    "Bixley",
                    "Gainsborough",
                    "Holywells",
                    "Nacton & Purdis Farm",
                    "Alexandra",
                    "St John's",
                    "Tower",
                    "Priory Heath"
                },
                result.AdminWard);
            Assert.AreEqual(new List<string> { "England" }, result.Country);
        }
    }
}