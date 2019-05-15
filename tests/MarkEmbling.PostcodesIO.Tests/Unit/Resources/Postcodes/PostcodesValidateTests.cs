using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesValidateTests
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
        public void Validate_request_method_is_get()
        {
            _postcodes.Validate("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<bool>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task ValidateAsync_request_method_is_get()
        {
            await _postcodes.ValidateAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<bool>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Validate_calls_appropriate_resource_url_with_postcode()
        {
            _postcodes.Validate("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<bool>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX/validate")));
        }

        [Test]
        public async Task ValidateAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _postcodes.ValidateAsync("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<bool>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX/validate")));
        }
    }
}
