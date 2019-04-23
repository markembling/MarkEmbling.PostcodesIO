using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesLookupTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Lookup_returns_populated_response()
        {
            var result = _postcodes.Lookup("GU1 1AA");

            AssertPopulatedPostcodeResult(result);
        }

        [Test]
        public async Task LookupAsync_returns_populated_response()
        {
            var result = await _postcodes.LookupAsync("GU1 1AA");

            AssertPopulatedPostcodeResult(result);
        }

        [Test]
        public void Lookup_returns_populated_codes_property()
        {
            var result = _postcodes.Lookup("GU1 1AA").Codes;

            AssertPopulatedCodes(result);
        }

        [Test]
        public async Task LookupAsync_returns_populated_codes_property()
        {
            var result = (await _postcodes.LookupAsync("GU1 1AA")).Codes;

            AssertPopulatedCodes(result);
        }

        [Test]
        public void Null_eastings_and_northings_are_handled()
        {
            // Channel island postcodes do not come with eastings or northings according to API docs
            var result = _postcodes.Lookup("JE2 4ST");

            Assert.IsNull(result.Eastings);
            Assert.IsNull(result.Northings);
        }

        [Test]
        public void Null_longitude_and_latitude_are_handled()
        {
            var result = _postcodes.Lookup("JE2 4ST");

            Assert.IsNull(result.Longitude);
            Assert.IsNull(result.Latitude);
        }

        private static void AssertPopulatedPostcodeResult(PostcodeResult result)
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

        private static void AssertPopulatedCodes(PostcodeCodesResult result)
        {
            Assert.AreEqual("E07000209", result.AdminDistrict);
            Assert.AreEqual("E10000030", result.AdminCounty);
            Assert.AreEqual("E05007293", result.AdminWard);
            Assert.AreEqual("E43000138", result.Parish);
            Assert.AreEqual("E38000214", result.CCG);
            Assert.AreEqual("UKJ25", result.NUTS);
        }
    }
}