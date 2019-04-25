using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Resources
{
    public class PostcodesResource
    {
        private readonly IRequestExecutor _requestExecutor;

        public PostcodesResource(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor;
        }

        public PostcodeResult Lookup(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return _requestExecutor.ExecuteRequest<PostcodeResult>(request);
        }

        public Task<PostcodeResult> LookupAsync(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return _requestExecutor.ExecuteRequestAsync<PostcodeResult>(request);
        }

        public IEnumerable<BulkQueryResult<string, PostcodeResult>> BulkLookup(IEnumerable<string> postcodes)
        {
            var request = CreateBulkLookupRequest(postcodes);
            return _requestExecutor.ExecuteRequest<List<BulkQueryResult<string, PostcodeResult>>>(request);
        }

        public Task<IEnumerable<BulkQueryResult<string, PostcodeResult>>> BulkLookupAsync(IEnumerable<string> postcodes)
        {
            var request = CreateBulkLookupRequest(postcodes);
            return _requestExecutor.ExecuteRequestAsync<List<BulkQueryResult<string, PostcodeResult>>>(request)
                .ContinueWith(t => t.Result as IEnumerable<BulkQueryResult<string, PostcodeResult>>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<PostcodeResult> ReverseGeocode(ReverseGeocodeQuery query)
        {
            var request = CreateReverseGeocodeRequest(query);
            return _requestExecutor.ExecuteRequest<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> ReverseGeocodeAsync(ReverseGeocodeQuery query)
        {
            var request = CreateReverseGeocodeRequest(query);
            return _requestExecutor.ExecuteRequestAsync<List<PostcodeResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>> BulkReverseGeocode(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkReverseGeocodeRequest(queries);
            return _requestExecutor.ExecuteRequest<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request);
        }

        public Task<IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>> BulkReverseGeocodeAsync(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkReverseGeocodeRequest(queries);
            return _requestExecutor.ExecuteRequestAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request)
                .ContinueWith(t => t.Result as IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<PostcodeResult> Query(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return _requestExecutor.ExecuteRequest<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> QueryAsync(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return _requestExecutor.ExecuteRequestAsync<List<PostcodeResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public bool Validate(string postcode)
        {
            var request = CreateValidateRequest(postcode);
            return _requestExecutor.ExecuteRequest<bool>(request);
        }

        public Task<bool> ValidateAsync(string postcode)
        {
            var request = CreateValidateRequest(postcode);
            return _requestExecutor.ExecuteRequestAsync<bool>(request);
        }

        public IEnumerable<PostcodeResult> Nearest(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(postcode, limit, radius);
            return _requestExecutor.ExecuteRequest<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> NearestAsync(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(postcode, limit, radius);
            return _requestExecutor.ExecuteRequestAsync<List<PostcodeResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<string> Autocomplete(string postcode, int? limit = null)
        {
            var request = CreateAutocompleteRequest(postcode, limit);
            return _requestExecutor.ExecuteRequest<List<string>>(request);
        }

        public Task<IEnumerable<string>> AutocompleteAsync(string postcode, int? limit = null)
        {
            var request = CreateAutocompleteRequest(postcode, limit);
            return _requestExecutor.ExecuteRequestAsync<List<string>>(request)
                .ContinueWith(t => t.Result as IEnumerable<string>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public PostcodeResult Random()
        {
            var request = CreateRandomRequest();
            return _requestExecutor.ExecuteRequest<PostcodeResult>(request);
        }

        public Task<PostcodeResult> RandomAsync()
        {
            var request = CreateRandomRequest();
            return _requestExecutor.ExecuteRequestAsync<PostcodeResult>(request);
        }

        private static RestRequest CreateLookupRequest(string postcode)
        {
            return new RestRequest(string.Format("postcodes/{0}", postcode), Method.GET, DataFormat.Json);
        }

        private static RestRequest CreateBulkLookupRequest(IEnumerable<string> postcodes)
        {
            var request = new RestRequest("postcodes", Method.POST, DataFormat.Json)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { postcodes });
            return request;
        }

        private static RestRequest CreateReverseGeocodeRequest(ReverseGeocodeQuery query)
        {
            var request = new RestRequest("postcodes", Method.GET, DataFormat.Json);
            request.AddParameter("lon", query.Longitude);
            request.AddParameter("lat", query.Latitude);
            if (query.Limit.HasValue) request.AddParameter("limit", query.Limit);
            if (query.Radius.HasValue) request.AddParameter("radius", query.Radius);
            if (query.WideSearch.HasValue) request.AddParameter("wideSearch", query.WideSearch);
            return request;
        }

        private static RestRequest CreateBulkReverseGeocodeRequest(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = new RestRequest("postcodes", Method.POST, DataFormat.Json)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonDotNetSerializer()
            };
            request.AddJsonBody(new { geolocations = queries });
            return request;
        }

        private static RestRequest CreateQueryRequest(string q, int? limit)
        {
            var request = new RestRequest("postcodes", Method.GET, DataFormat.Json);
            request.AddQueryParameter("q", q);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return request;
        }

        private static RestRequest CreateValidateRequest(string postcode)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/validate", postcode), Method.GET, DataFormat.Json);
            return request;
        }

        private static RestRequest CreateNearestRequest(string postcode, int? limit, int? radius)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/nearest", postcode), Method.GET, DataFormat.Json);
            if (limit.HasValue) request.AddParameter("limit", limit);
            if (radius.HasValue) request.AddParameter("radius", radius);
            return request;
        }

        private static RestRequest CreateAutocompleteRequest(string postcode, int? limit)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/autocomplete", postcode), Method.GET, DataFormat.Json);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return request;
        }

        private static RestRequest CreateRandomRequest()
        {
            var request = new RestRequest("random/postcodes", Method.GET, DataFormat.Json);
            return request;
        }
    }
}
