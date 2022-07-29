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
            Assert.AreEqual("Guildford", result.ParliamentaryConstituency);
            Assert.AreEqual("South East", result.EuropeanElectoralRegion);
            Assert.AreEqual("Surrey", result.PrimaryCareTrust);
            Assert.AreEqual("South East", result.Region);
            Assert.AreEqual("Guildford 015A", result.LSOA);
            Assert.AreEqual("Guildford 015", result.MSOA);
            Assert.AreEqual("West Surrey", result.NUTS);
            Assert.AreEqual("1AA", result.InCode);
            Assert.AreEqual("GU1", result.OutCode);
            Assert.AreEqual("Guildford", result.AdminDistrict);
            Assert.AreEqual("Guildford, unparished area", result.Parish);
            Assert.AreEqual("Surrey", result.AdminCounty);
            Assert.AreEqual("Friary and St Nicolas", result.AdminWard);
            Assert.AreEqual("NHS Surrey Heartlands", result.CCG);
            Assert.NotNull(result.Codes);
        }

        private static void TestOutwardCode_Lookup_returns_populated_responseResult(OutwardCodeResult result)
        {
            Assert.AreEqual("IP3", result.Outcode);
            Assert.AreEqual(1.18801782268579, result.Longitude);
            Assert.AreEqual(52.0413507588004, result.Latitude);
            Assert.AreEqual(242903, result.Northings);
            Assert.AreEqual(618733, result.Eastings);
            Assert.AreEqual(new List<string>() { "Ipswich", "East Suffolk" }, result.AdminDistrict);
            Assert.AreEqual(new List<string>() { "Ipswich, unparished area", "Rushmere St. Andrew", "Purdis Farm" }, result.Parish);
            Assert.AreEqual(new List<string>() { "Suffolk" }, result.AdminCounty);
            Assert.AreEqual(new List<string>() { "Bixley", "Martlesham & Purdis Farm", "Gainsborough", "Holywells", "Rushmere St Andrew", "Alexandra", "St John's", "Priory Heath" }, result.AdminWard);    
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

        private static void TestLookup_returns_populated_codes_propertyResult(Codes result)
        {
            Assert.AreEqual("E07000209", result.AdminDistrict);
            Assert.AreEqual("E10000030", result.AdminCounty);
            Assert.AreEqual("E05007293", result.AdminWard);
            Assert.AreEqual("E43000138", result.Parish);
            Assert.AreEqual("E38000246", result.CCG);
        }
    }
}