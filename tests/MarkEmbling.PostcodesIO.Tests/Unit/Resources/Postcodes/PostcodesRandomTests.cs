using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesRandomTests
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
        public void Random_request_method_is_get()
        {
            _postcodes.Random();
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task RandomAsync_request_method_is_get()
        {
            await _postcodes.RandomAsync();
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Random_calls_appropriate_resource_url()
        {
            _postcodes.Random();
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "random/postcodes")));
        }

        [Test]
        public async Task RandomAsync_calls_appropriate_resource_url()
        {
            await _postcodes.RandomAsync();
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<PostcodeResult>(
                    It.Is<RestRequest>(req => req.Resource == "random/postcodes")));
        }
    }
}
