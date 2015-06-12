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
            var result = _client.BulkLookup(new[]{"GU1 1AA", "GU1 1AB"}).ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("GU1 1AA", result[0].Query);
            Assert.AreEqual("GU1 1AB", result[0].Query);
        }
    }
}