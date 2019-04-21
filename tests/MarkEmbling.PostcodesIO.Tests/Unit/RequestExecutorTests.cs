using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class RequestExecutorTests
    {
        [Test]
        public void ExecuteRequest_calls_Execute_method_on_client()
        {
            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();

            clientMock.Setup(x => x.Execute<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object)
                .Verifiable();
            responseMock.SetupGet(x => x.Data).Returns(new RawResult<object>());

            var sut = new RequestExecutor(clientMock.Object);
            sut.ExecuteRequest<object>(new RestRequest());

            clientMock.Verify();
        }

        [Test]
        public void ExecuteRequest_returns_the_result_when_there_is_one()
        {
            var resultData = new object();

            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();
            var rawResultMock = new Mock<RawResult<object>>();

            clientMock.Setup(x => x.Execute<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object);
            responseMock.SetupGet(x => x.Data).Returns(rawResultMock.Object);
            rawResultMock.SetupGet(x => x.Result).Returns(resultData);

            var sut = new RequestExecutor(clientMock.Object);
            var result = sut.ExecuteRequest<object>(new RestRequest());

            Assert.AreSame(resultData, result);
        }

        [Test]
        public void ExecuteRequest_throws_exception_when_response_is_null()
        {
            var resultData = new object();

            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();

            clientMock.Setup(x => x.Execute<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object);
            responseMock.SetupGet(x => x.Data).Returns((RawResult<object>)null);

            var sut = new RequestExecutor(clientMock.Object);
            Assert.Throws<PostcodesIOEmptyResponseException>(
                () => sut.ExecuteRequest<object>(new RestRequest()));
        }

        [Test]
        public void ExecuteRequest_throws_when_request_results_in_error()
        {
            var expectedInnerException = new Exception();

            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();

            clientMock.Setup(x => x.Execute<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object);
            responseMock.SetupGet(x => x.ErrorException).Returns(expectedInnerException);

            var sut = new RequestExecutor(clientMock.Object);
            var resultException = Assert.Throws<PostcodesIOApiException>(
                () => sut.ExecuteRequest<object>(new RestRequest()));
            Assert.AreSame(expectedInnerException, resultException.InnerException);
        }

        [Test]
        public void Execute_methods_return_empty_collection_instead_of_null()
        {
            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<List<object>>>>();
            var rawResultMock = new Mock<RawResult<List<object>>>();

            clientMock.Setup(x => x.Execute<RawResult<List<object>>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object);
            responseMock.SetupGet(x => x.Data).Returns(rawResultMock.Object);
            rawResultMock.SetupGet(x => x.Result).Returns((List<object>)null);

            var sut = new RequestExecutor(clientMock.Object);
            var result = sut.ExecuteRequest<List<object>>(new RestRequest());

            Assert.AreEqual(new List<object>(), result);
        }

        [Test]
        public void Execute_methods_return_null_if_its_not_a_collection()
        {
            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();
            var rawResultMock = new Mock<RawResult<object>>();

            clientMock.Setup(x => x.Execute<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(responseMock.Object);
            responseMock.SetupGet(x => x.Data).Returns(rawResultMock.Object);
            rawResultMock.SetupGet(x => x.Result).Returns(null);

            var sut = new RequestExecutor(clientMock.Object);
            var result = sut.ExecuteRequest<object>(new RestRequest());

            Assert.IsNull(result);
        }

        [Test]
        public async Task ExecuteRequestAsync_calls_ExecuteTaskAsync_on_client()
        {
            var clientMock = new Mock<IRestClient>();
            var responseMock = new Mock<IRestResponse<RawResult<object>>>();

            clientMock.Setup(x => x.ExecuteTaskAsync<RawResult<object>>(It.IsAny<IRestRequest>()))
                .Returns(Task.FromResult(responseMock.Object))
                .Verifiable();
            responseMock.SetupGet(x => x.Data).Returns(new RawResult<object>());

            var sut = new RequestExecutor(clientMock.Object);
            await sut.ExecuteRequestAsync<object>(new RestRequest());

            clientMock.Verify();
        }

        
    }
}
