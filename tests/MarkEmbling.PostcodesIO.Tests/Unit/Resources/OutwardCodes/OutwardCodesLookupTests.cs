using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.OutwardCodes
{
    [TestFixture]
    public class OutwardCodesLookupTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _outcodes = new OutwardCodesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void Lookup_request_method_is_get()
        {
            _outcodes.Lookup("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<OutwardCodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task LookupAsync_request_method_is_get()
        {
            await _outcodes.LookupAsync("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<OutwardCodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Lookup_calls_appropriate_resource_url_with_code()
        {
            _outcodes.Lookup("XX1");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<OutwardCodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes/XX1")));
        }

        [Test]
        public async Task LookupAsync_calls_appropriate_resource_url_with_code()
        {
            await _outcodes.LookupAsync("XX1");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<OutwardCodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes/XX1")));
        }
    }
}
