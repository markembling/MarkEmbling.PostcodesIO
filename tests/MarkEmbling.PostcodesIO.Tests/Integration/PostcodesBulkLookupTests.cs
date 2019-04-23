using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesBulkLookupTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void BulkLookup_returns_results()
        {
            var result = _postcodes.BulkLookup(new[]{"GU1 1AA", "GU1 1AB", "GU1 1AD"}).ToList();

            AssertResults(result);
        }

        [Test]
        public async Task BulkLookupAsync_returns_results()
        {
            var result = (await _postcodes.BulkLookupAsync(new[] { "GU1 1AA", "GU1 1AB", "GU1 1AD" })).ToList();

            AssertResults(result);
        }

        private static void AssertResults(List<BulkQueryResult<string, PostcodeResult>> result)
        {
            Assert.AreEqual(3, result.Count);
            // The results come back in no particular order, so we must check for 
            // existence of both but make no assumptions about the order they 
            // may have come back in.
            Assert.True(result.Any(x => x.Query == "GU1 1AA"));
            Assert.True(result.Any(x => x.Query == "GU1 1AB"));
            Assert.True(result.Any(x => x.Query == "GU1 1AD"));
        }
    }
}