using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Places
{
    [TestFixture]
    public class PlacesQueryTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private PlacesResource _places;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _places = new PlacesResource(_requestExecutorMock.Object);
        }

        [Test]
        public void Query_request_method_is_get()
        {
            _places.Query("query");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task QueryAsync_request_method_is_get()
        {
            await _places.QueryAsync("query");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Query_calls_appropriate_resource_url()
        {
            _places.Query("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Resource == "places")));
        }

        [Test]
        public async Task QueryAsync_calls_appropriate_resource_url()
        {
            await _places.QueryAsync("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Resource == "places")));
        }

        [Test]
        public void Query_basic_query_adds_q_param_with_correct_value()
        {
            _places.Query("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "q" && (string)p.Value == "query"))));
        }

        [Test]
        public async Task QueryAsync_basic_query_adds_q_param_with_correct_value()
        {
            await _places.QueryAsync("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "q" && (string)p.Value == "query"))));
        }

        [Test]
        public void Query_basic_query_has_no_other_params()
        {
            _places.Query("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 1)));
        }

        [Test]
        public async Task QueryAsync_basic_query_has_no_other_params()
        {
            await _places.QueryAsync("query");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 1)));
        }

        [Test]
        public void Query_with_limit_adds_limit_param_with_correct_value()
        {
            _places.Query("query", 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public async Task QueryAsync_with_limit_adds_limit_param_with_correct_value()
        {
            await _places.QueryAsync("query", 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public void Query_with_limit_has_two_params()
        {
            _places.Query("query", 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 2)));
        }

        [Test]
        public async Task QueryAsync_with_limit_has_two_params()
        {
            await _places.QueryAsync("query", 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PlaceResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 2)));
        }
    }
}
