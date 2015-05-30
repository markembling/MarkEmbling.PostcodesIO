using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests {
    [TestFixture]
    public class PostcodesIOClientTests {
        [Test]
        public void Lookup_returns_appropriate_response() {
            var client = new PostcodesIOClient();
            var result = client.Lookup("GU1 1AA");
        }
    }
}