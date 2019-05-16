using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesBulkLookupTests
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
        public void BulkLookup_request_method_is_post()
        {
            _postcodes.BulkLookup(new[] { "XX1 1XX", "XX2 2XX" });
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<BulkQueryResult<string, PostcodeResult>>>(
                    It.Is<RestRequest>(req => req.Method == Method.POST)));
        }

        [Test]
        public async Task BulkLookupAsync_request_method_is_post()
        {
            await _postcodes.BulkLookupAsync(new[] { "XX1 1XX", "XX2 2XX" });
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<BulkQueryResult<string, PostcodeResult>>>(
                    It.Is<RestRequest>(req => req.Method == Method.POST)));
        }

        [Test]
        public void BulkLookup_calls_appropriate_resource_url()
        {
            _postcodes.BulkLookup(new[] { "XX1 1XX", "XX2 2XX" });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<BulkQueryResult<string, PostcodeResult>>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        [Test]
        public async Task BulkLookupAsync_calls_appropriate_resource_url()
        {
            await _postcodes.BulkLookupAsync(new[] { "XX1 1XX", "XX2 2XX" });
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<BulkQueryResult<string, PostcodeResult>>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes")));
        }

        // TODO: test that body is set appropriately
    }
}
