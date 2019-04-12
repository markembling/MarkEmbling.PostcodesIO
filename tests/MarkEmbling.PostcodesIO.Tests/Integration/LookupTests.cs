using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class LookupTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Lookup_returns_populated_response()
        {
            var result = _client.Lookup("GU1 1AA");

            TestLookup_returns_populated_responseResult(result);
        }

        [Test]
        public void OutwardCode_Lookup_returns_populated_response()
        {
            var result = _client.OutwardCodeLookup("IP3");

            TestOutwardCode_Lookup_returns_populated_responseResult(result);
        }

        [Test]
        public async Task Lookup_returns_populated_response_async()
        {
            var result = await _client.LookupAsync("GU1 1AA");

            TestLookup_returns_populated_responseResult(result);
        }

        private static void TestLookup_returns_populated_responseResult(PostcodeResult result)
        {
            Assert.AreEqual("GU1 1AA", result.Postcode);
            Assert.AreEqual(1, result.Quality);
            Assert.AreEqual(499049, result.Eastings);
            Assert.AreEqual(150522, result.Northings);
            Assert.AreEqual("England", result.Country);
            Assert.AreEqual("South East Coast", result.NHSHealthAuthority);
            Assert.AreEqual(-0.582332, result.Longitude);
            Assert.AreEqual(51.245283, result.Latitude);
            Assert.AreEqual("South East", result.EuropeanElectoralRegion);
            Assert.AreEqual("Surrey", result.PrimaryCareTrust);
            Assert.AreEqual("South East", result.Region);
            Assert.AreEqual("Guildford 015A", result.LSOA);
            Assert.AreEqual("Guildford 015", result.MSOA);
            Assert.AreEqual("1AA", result.InCode);
            Assert.AreEqual("GU1", result.OutCode);
            Assert.AreEqual("Guildford", result.ParliamentaryConstituency);
            Assert.AreEqual("Guildford", result.AdminDistrict);
            Assert.AreEqual("Guildford, unparished area", result.Parish);
            Assert.AreEqual("Surrey", result.AdminCounty);
            Assert.AreEqual("Friary and St Nicolas", result.AdminWard);
            Assert.AreEqual("Guildford South West", result.CED);
            Assert.AreEqual("NHS Guildford and Waverley", result.CCG);
            Assert.AreEqual("West Surrey", result.NUTS);
            Assert.NotNull(result.Codes);
        }

        private static void TestOutwardCode_Lookup_returns_populated_responseResult(OutwardCodeResult result)
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

        [Test]
        public void Lookup_returns_populated_codes_property()
        {
            var result = _client.Lookup("GU1 1AA").Codes;

            TestLookup_returns_populated_codes_propertyResult(result);
        }

        [Test]
        public async Task Lookup_returns_populated_codes_property_async()
        {
            var result = (await _client.LookupAsync("GU1 1AA")).Codes;

            TestLookup_returns_populated_codes_propertyResult(result);
        }

        private static void TestLookup_returns_populated_codes_propertyResult(PostcodeCodesResult result)
        {
            Assert.AreEqual("E07000209", result.AdminDistrict);
            Assert.AreEqual("E10000030", result.AdminCounty);
            Assert.AreEqual("E05007293", result.AdminWard);
            Assert.AreEqual("E43000138", result.Parish);
            Assert.AreEqual("E38000214", result.CCG);
            Assert.AreEqual("UKJ25", result.NUTS);
        }

        [Test]
        public void Null_eastings_and_northings_are_handled()
        {
            // Channel island postcodes do not come with eastings or northings according to API docs
            var result = _client.Lookup("JE2 4ST");

            Assert.IsNull(result.Eastings);
            Assert.IsNull(result.Northings);
        }
    }
}