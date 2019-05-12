using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.OutwardCodes
{
    [TestFixture]
    public class OutwardCodesNearestTests
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
        public void Nearest_request_method_is_get()
        {
            _outcodes.Nearest("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task NearestAsync_request_method_is_get()
        {
            await _outcodes.NearestAsync("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Nearest_calls_appropriate_resource_url_with_code()
        {
            _outcodes.Nearest("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes/XX1/nearest")));
        }

        [Test]
        public async Task NearestAsync_calls_appropriate_resource_url_with_code()
        {
            await _outcodes.NearestAsync("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Resource == "outcodes/XX1/nearest")));
        }

        [Test]
        public void Nearest_simple_query_has_no_query_params()
        {
            _outcodes.Nearest("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public async Task NearestAsync_simple_query_has_no_query_params()
        {
            await _outcodes.NearestAsync("XX1");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public void Nearest_with_limit_adds_limit_param_with_correct_value()
        {
            _outcodes.Nearest("XX1", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public async Task NearestAsync_with_limit_adds_limit_param_with_correct_value()
        {
            await _outcodes.NearestAsync("XX1", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public void Nearest_with_radius_adds_radius_param_with_correct_value()
        {
            _outcodes.Nearest("XX1", radius: 2000);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == 2000))));
        }

        [Test]
        public async Task NearestAsync_with_radius_adds_radius_param_with_correct_value()
        {
            await _outcodes.NearestAsync("XX1", radius: 2000);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<OutwardCodeResult>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "radius" && (int)p.Value == 2000))));
        }
    }
}
