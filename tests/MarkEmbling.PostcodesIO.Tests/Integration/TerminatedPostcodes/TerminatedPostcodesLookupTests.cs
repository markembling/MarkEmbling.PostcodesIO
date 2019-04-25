using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.TerminatedPostcodes
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class TerminatedPostcodesLookupTests
    {
        private TerminatedPostcodesResource _terminated;

        [SetUp]
        public void Setup()
        {
            _terminated = new TerminatedPostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void Lookup_returns_populated_response()
        {
            var result = _terminated.Lookup("LS14 6PF");

            AssertPopulatedTerminatedPostcodeResult(result);
        }

        [Test]
        public async Task LookupAsync_returns_populated_response()
        {
            var result = await _terminated.LookupAsync("LS14 6PF");

            AssertPopulatedTerminatedPostcodeResult(result);
        }

        private static void AssertPopulatedTerminatedPostcodeResult(TerminatedPostcodeResult result)
        {
            Assert.AreEqual(53.820736, result.Latitude);
            Assert.AreEqual(-1.460518, result.Longitude);
            Assert.AreEqual(7, result.MonthTerminated);
            Assert.AreEqual("LS14 6PF", result.Postcode);
            Assert.AreEqual(2013, result.YearTerminated);
        }
    }
}
