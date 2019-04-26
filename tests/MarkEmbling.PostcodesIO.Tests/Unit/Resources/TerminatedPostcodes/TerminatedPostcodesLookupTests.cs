using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.TerminatedPostcodes
{
    [TestFixture]
    public class TerminatedPostcodesLookupTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private TerminatedPostcodesResource _terminated;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _terminated = new TerminatedPostcodesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void Lookup_request_method_is_get()
        {
            _terminated.Lookup("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<TerminatedPostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task LookupAsync_request_method_is_get()
        {
            await _terminated.LookupAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<TerminatedPostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Lookup_calls_appropriate_resource_url_with_postcode()
        {
            _terminated.Lookup("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<TerminatedPostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "terminated_postcodes/XX1 1XX")));
        }

        [Test]
        public async Task LookupAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _terminated.LookupAsync("XX1 1XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<TerminatedPostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "terminated_postcodes/XX1 1XX")));
        }
    }
}
