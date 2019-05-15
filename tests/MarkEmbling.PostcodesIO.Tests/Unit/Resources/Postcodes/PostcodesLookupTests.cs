using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesLookupTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _postcodes = new PostcodesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void Lookup_request_method_is_get()
        {
            _postcodes.Lookup("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task LookupAsync_request_method_is_get()
        {
            await _postcodes.LookupAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Lookup_calls_appropriate_resource_url_with_postcode()
        {
            _postcodes.Lookup("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX")));
        }

        [Test]
        public async Task LookupAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _postcodes.LookupAsync("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX")));
        }
    }
}
