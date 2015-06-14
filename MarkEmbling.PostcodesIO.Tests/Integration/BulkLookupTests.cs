using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Integration {
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class BulkLookupTests {
        private PostcodesIOClient _client;

        [SetUp]
        public void Setup() {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void BulkLookup_returns_results() {
            var result = _client.BulkLookup(new[]{"GU1 1AA", "GU1 1AB", "GU1 1AD"}).ToList();

            Assert.AreEqual(3, result.Count());
            // The results come back in no particular order, so we must check for 
            // existence of both but make no assumptions about the order they 
            // may have come back in.
            Assert.True(result.Any(x => x.Query == "GU1 1AA"));
            Assert.True(result.Any(x => x.Query == "GU1 1AB"));
            Assert.True(result.Any(x => x.Query == "GU1 1AD"));
        }
    }
}