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
            Assert.That(result.Postcode, Is.EqualTo("GU1 1AA"));
            Assert.That(result.Quality, Is.EqualTo(1));
            Assert.That(result.Eastings, Is.EqualTo(499049));
            Assert.That(result.Northings, Is.EqualTo(150522));
            Assert.That(result.Country, Is.EqualTo("England"));
            Assert.That(result.NHSHealthAuthority, Is.EqualTo("South East Coast"));
            Assert.That(result.Longitude, Is.EqualTo(-0.582332));
            Assert.That(result.Latitude, Is.EqualTo(51.245283));
            Assert.That(result.ParliamentaryConstituency, Is.EqualTo("Guildford"));
            Assert.That(result.EuropeanElectoralRegion, Is.EqualTo("South East"));
            Assert.That(result.PrimaryCareTrust, Is.EqualTo("Surrey"));
            Assert.That(result.Region, Is.EqualTo("South East"));
            Assert.That(result.LSOA, Is.EqualTo("Guildford 015A"));
            Assert.That(result.MSOA, Is.EqualTo("Guildford 015"));
            Assert.That(result.NUTS, Is.EqualTo("Guildford"));
            Assert.That(result.InCode, Is.EqualTo("1AA"));
            Assert.That(result.OutCode, Is.EqualTo("GU1"));
            Assert.That(result.AdminDistrict, Is.EqualTo("Guildford"));
            Assert.That(result.Parish, Is.EqualTo("Guildford, unparished area"));
            Assert.That(result.AdminCounty, Is.EqualTo("Surrey"));
            Assert.That(result.AdminWard, Is.EqualTo("Stoke"));
            Assert.That(result.CED, Is.EqualTo("Guildford South West"));
            Assert.That(result.CCG, Is.EqualTo("NHS Surrey Heartlands"));
            Assert.That(result.Codes, Is.Not.Null);
        }

        private static void TestOutwardCode_Lookup_returns_populated_responseResult(OutwardCodeResult result)
        {
            Assert.That(result.Outcode, Is.EqualTo("IP3"));
            Assert.That(result.Northings, Is.EqualTo(242900));
            Assert.That(result.Eastings, Is.EqualTo(618740));
            Assert.That(result.AdminCounty, Is.EqualTo(new List<string>() { "Suffolk" }));
            Assert.That(result.AdminDistrict, Is.EqualTo(new List<string>() { "Ipswich", "East Suffolk" }));
            Assert.That(result.AdminWard, Is.EqualTo(new List<string>() { "Bixley", "Martlesham & Purdis Farm", "Gainsborough", "Holywells", "Rushmere St Andrew", "Alexandra", "St John's", "Priory Heath" }));
            Assert.That(result.Longitude, Is.EqualTo(1.188121282722514));
            Assert.That(result.Latitude, Is.EqualTo(52.041320951570555));
            Assert.That(result.Country, Is.EqualTo(new List<string>() { "England" }));
            Assert.That(result.Parish, Is.EqualTo(new List<string>() { "Ipswich, unparished area", "Rushmere St. Andrew", "Purdis Farm" }));
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
            Assert.That(result.AdminDistrict, Is.EqualTo("E07000209"));
            Assert.That(result.AdminCounty, Is.EqualTo("E10000030"));
            Assert.That(result.AdminWard, Is.EqualTo("E05014959"));
            Assert.That(result.Parish, Is.EqualTo("E43000138"));
            Assert.That(result.CCG, Is.EqualTo("E38000264"));
            Assert.That(result.CCGId, Is.EqualTo("92A"));
            Assert.That(result.CED, Is.EqualTo("E58001497"));
            Assert.That(result.NUTS, Is.EqualTo("TLJ25"));
            Assert.That(result.LAU2, Is.EqualTo("E07000209"));
            Assert.That(result.LSOA, Is.EqualTo("E01030452"));
            Assert.That(result.MSOA, Is.EqualTo("E02006358"));
            Assert.That(result.PFA, Is.EqualTo("E23000031"));
        }
    }
}