using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Resources
{
    public class PlacesResource
    {
        private readonly IRequestExecutor _requestExecutor;

        public PlacesResource(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor;
        }

        public PlaceResult Lookup(string code)
        {
            var request = CreateLookupRequest(code);
            return _requestExecutor.ExecuteRequest<PlaceResult>(request);
        }

        public Task<PlaceResult> LookupAsync(string code)
        {
            var request = CreateLookupRequest(code);
            return _requestExecutor.ExecuteRequestAsync<PlaceResult>(request);
        }

        public IEnumerable<PlaceResult> Query(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return _requestExecutor.ExecuteRequest<List<PlaceResult>>(request);
        }

        public Task<IEnumerable<PlaceResult>> QueryAsync(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return _requestExecutor.ExecuteRequestAsync<List<PlaceResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<PlaceResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public PlaceResult Random()
        {
            var request = CreateRandomRequest();
            return _requestExecutor.ExecuteRequest<PlaceResult>(request);
        }

        public Task<PlaceResult> RandomAsync()
        {
            var request = CreateRandomRequest();
            return _requestExecutor.ExecuteRequestAsync<PlaceResult>(request);
        }

        private static RestRequest CreateLookupRequest(string code)
        {
            var request = new RestRequest(string.Format("places/{0}", code), Method.GET, DataFormat.Json);
            return request;
        }

        private static RestRequest CreateQueryRequest(string q, int? limit)
        {
            var request = new RestRequest("places", Method.GET, DataFormat.Json);
            request.AddQueryParameter("q", q);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return request;
        }

        private static RestRequest CreateRandomRequest()
        {
            var request = new RestRequest("random/places", Method.GET, DataFormat.Json);
            return request;
        }
    }
}
