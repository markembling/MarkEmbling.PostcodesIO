using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Integration {
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesIOClientTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Lookup_returns_populated_response() {
            var result = _client.Lookup("GU1 1AA");

            Assert.AreEqual("GU1 1AA", result.Postcode);
            Assert.AreEqual(1, result.Quality);
            Assert.AreEqual(499050, result.Eastings);
            Assert.AreEqual(150523, result.Northings);
            Assert.AreEqual("England", result.Country);
            Assert.AreEqual("South East Coast", result.NHSHealthAuthority);
            Assert.AreEqual(-0.58231794275613, result.Longitude);
            Assert.AreEqual(51.2452924089757, result.Latitude);
            Assert.AreEqual("Guildford", result.ParliamentaryConstituency);
            Assert.AreEqual("South East", result.EuropeanElectoralRegion);
            Assert.AreEqual("Surrey", result.PrimaryCareTrust);
            Assert.AreEqual("South East", result.Region);
            Assert.AreEqual("Guildford 015A", result.LSOA);
            Assert.AreEqual("Guildford 015", result.MSOA);
            Assert.AreEqual("Friary and St Nicolas", result.NUTS);
            Assert.AreEqual("1AA", result.InCode);
            Assert.AreEqual("GU1", result.OutCode);
            Assert.AreEqual("Guildford", result.AdminDistrict);
            Assert.AreEqual("Guildford, unparished area", result.Parish);
            Assert.AreEqual("Surrey", result.AdminCounty);
            Assert.AreEqual("Friary and St Nicolas", result.AdminWard);
            Assert.AreEqual("NHS Guildford and Waverley", result.CCG);
            Assert.NotNull(result.Codes);
        }

        [Test]
        public void Lookup_returns_populated_codes_property() {
            var result = _client.Lookup("GU1 1AA").Codes;

            Assert.AreEqual("E07000209", result.AdminDistrict);
            Assert.AreEqual("E10000030", result.AdminCounty);
            Assert.AreEqual("E05007293", result.AdminWard);
            Assert.AreEqual("E43000138", result.Parish);
            Assert.AreEqual("E38000067", result.CCG);
        }
    }
}