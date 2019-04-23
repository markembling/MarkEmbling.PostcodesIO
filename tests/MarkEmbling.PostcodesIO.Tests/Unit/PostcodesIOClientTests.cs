using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class PostcodesIOClientTests
    {
        PostcodesIOClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new PostcodesIOClient();
        }

        [Test]
        public void Postcodes_property_returns_instance_of_PostcodesResource()
        {
            Assert.IsInstanceOf<PostcodesResource>(_client.Postcodes);
        }

        [Test]
        public void TerminatedPostcodes_property_returns_instance_of_TerminatedPostcodesResource()
        {
            Assert.IsInstanceOf<TerminatedPostcodesResource>(_client.TerminatedPostcodes);
        }

        [Test]
        public void OutwardCodes_property_returns_instance_of_OutwardCodesResource()
        {
            Assert.IsInstanceOf<OutwardCodesResource>(_client.OutwardCodes);
        }

        [Test]
        public void Places_property_returns_instance_of_PlacesResource()
        {
            Assert.IsInstanceOf<PlacesResource>(_client.Places);
        }
    }
}
