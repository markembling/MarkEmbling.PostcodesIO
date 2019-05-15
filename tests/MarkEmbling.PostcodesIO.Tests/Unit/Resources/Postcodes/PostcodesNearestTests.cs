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

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesNearestTests
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
        public void Nearest_request_method_is_get()
        {
            _postcodes.Nearest("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task NearestAsync_request_method_is_get()
        {
            await _postcodes.NearestAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Nearest_calls_appropriate_resource_url_with_postcode()
        {
            _postcodes.Nearest("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX/nearest")));
        }

        [Test]
        public async Task NearestAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _postcodes.NearestAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX1 1XX/nearest")));
        }

        [Test]
        public void Nearest_simple_query_has_no_query_params()
        {
            _postcodes.Nearest("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public async Task NearestAsync_simple_query_has_no_query_params()
        {
            await _postcodes.NearestAsync("XX1 1XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public void Nearest_with_limit_adds_limit_param_with_correct_value()
        {
            _postcodes.Nearest("XX1 1XX", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public async Task NearestAsync_with_limit_adds_limit_param_with_correct_value()
        {
            await _postcodes.NearestAsync("XX1 1XX", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public void Nearest_with_radius_adds_radius_param_with_correct_value()
        {
            _postcodes.Nearest("XX1 1XX", radius: 2000);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == 2000))));
        }

        [Test]
        public async Task NearestAsync_with_radius_adds_radius_param_with_correct_value()
        {
            await _postcodes.NearestAsync("XX1 1XX", radius: 2000);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<PostcodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == 2000))));
        }
    }
}
