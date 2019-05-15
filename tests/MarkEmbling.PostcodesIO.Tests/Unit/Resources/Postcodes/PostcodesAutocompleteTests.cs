using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.Postcodes
{
    [TestFixture]
    public class PostcodesAutocompleteTests
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
        public void Autocomplete_request_method_is_get()
        {
            _postcodes.Autocomplete("XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<string>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public async Task AutocompleteAsync_request_method_is_get()
        {
            await _postcodes.AutocompleteAsync("XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<string>>(
                    It.Is<RestRequest>(req => req.Method == Method.GET)));
        }

        [Test]
        public void Autocomplete_calls_appropriate_resource_url_with_postcode()
        {
            _postcodes.Autocomplete("XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<string>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX/autocomplete")));
        }

        [Test]
        public async Task AutocompleteAsync_calls_appropriate_resource_url_with_postcode()
        {
            await _postcodes.AutocompleteAsync("XX");
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<string>>(
                    It.Is<RestRequest>(req => req.Resource == "postcodes/XX/autocomplete")));
        }

        [Test]
        public void Autocomplete_simple_query_has_no_query_params()
        {
            _postcodes.Autocomplete("XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequest<List<string>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public async Task AutocompleteAsync_simple_query_has_no_query_params()
        {
            await _postcodes.AutocompleteAsync("XX");
            _requestExecutorMock.Verify(
                x => x.ExecuteRequestAsync<List<string>>(
                    It.Is<RestRequest>(req => req.Parameters.Count == 0)));
        }

        [Test]
        public void Autocomplete_with_limit_adds_limit_param_with_correct_value()
        {
            _postcodes.Autocomplete("XX", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequest<List<string>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }

        [Test]
        public async Task AutocompleteAsync_with_limit_adds_limit_param_with_correct_value()
        {
            await _postcodes.AutocompleteAsync("XX", limit: 10);
            _requestExecutorMock.Verify(x =>
                x.ExecuteRequestAsync<List<string>>(
                    It.Is<RestRequest>(req => req.Parameters.Any(p => p.Name == "limit" && (int)p.Value == 10))));
        }
    }
}
