using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;

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

        [Test]
        public void Client_can_take_custom_RequestExecutor_and_resource_calls_use_it()
        {
            var mockRequestExecutor = new Mock<IRequestExecutor>();
            var client = new PostcodesIOClient(mockRequestExecutor.Object);

            client.Postcodes.Lookup("POSTCODE");
            client.TerminatedPostcodes.Lookup("TERMCODE");
            client.OutwardCodes.Lookup("OUTCODE");
            client.Places.Lookup("CODE");

            mockRequestExecutor.Verify(x => x.ExecuteRequest<PostcodeResult>(It.IsAny<RestRequest>()));
            mockRequestExecutor.Verify(x => x.ExecuteRequest<TerminatedPostcodeResult>(It.IsAny<RestRequest>()));
            mockRequestExecutor.Verify(x => x.ExecuteRequest<OutwardCodeResult>(It.IsAny<RestRequest>()));
            mockRequestExecutor.Verify(x => x.ExecuteRequest<PlaceResult>(It.IsAny<RestRequest>()));
        }
    }
}
