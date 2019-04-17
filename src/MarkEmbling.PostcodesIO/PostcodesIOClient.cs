using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO
{
    public class PostcodesIOClient : IPostcodesIOClient
    {
        private readonly string _endpoint;

        public PostcodesIOClient(string endpoint = "https://api.postcodes.io")
        {
            _endpoint = endpoint;
        }

        public PostcodeResult Lookup(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return Execute<PostcodeResult>(request);
        }

        public Task<PostcodeResult> LookupAsync(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return ExecuteAsync<PostcodeResult>(request);
        }

        public IEnumerable<BulkQueryResult<string, PostcodeResult>> BulkLookup(IEnumerable<string> postcodes)
        {
            var request = CreateBulkLookupRequest(postcodes);
            return Execute<List<BulkQueryResult<string, PostcodeResult>>>(request);
        }

        public Task<IEnumerable<BulkQueryResult<string, PostcodeResult>>> BulkLookupAsync(IEnumerable<string> postcodes)
        {
            var request = CreateBulkLookupRequest(postcodes);
            return ExecuteAsync<List<BulkQueryResult<string, PostcodeResult>>>(request).ContinueWith(t => t.Result as IEnumerable<BulkQueryResult<string, PostcodeResult>>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<PostcodeResult> ReverseGeocode(ReverseGeocodeQuery query)
        {
            var request = CreateReverseGeocodeRequest(query);
            return Execute<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> ReverseGeocodeAsync(ReverseGeocodeQuery query)
        {
            var request = CreateReverseGeocodeRequest(query);
            return ExecuteAsync<List<PostcodeResult>>(request).ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>> BulkReverseGeocode(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkReverseGeocodeRequest(queries);
            return Execute<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request);
        }

        public Task<IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>> BulkReverseGeocodeAsync(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkReverseGeocodeRequest(queries);
            return ExecuteAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request).ContinueWith(t => t.Result as IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<PostcodeResult> Query(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return Execute<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> QueryAsync(string q, int? limit = null)
        {
            var request = CreateQueryRequest(q, limit);
            return ExecuteAsync<List<PostcodeResult>>(request).ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public bool Validate(string postcode)
        {
            var request = CreateValidateRequest(postcode);
            return Execute<bool>(request);
        }

        public Task<bool> ValidateAsync(string postcode)
        {
            var request = CreateValidateRequest(postcode);
            return ExecuteAsync<bool>(request);
        }

        public IEnumerable<PostcodeResult> Nearest(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(postcode, limit, radius);
            return Execute<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> NearestAsync(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(postcode, limit, radius);
            return ExecuteAsync<List<PostcodeResult>>(request).ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<string> Autocomplete(string postcode, int? limit = null)
        {
            var request = CreateAutocompleteRequest(postcode, limit);
            return Execute<List<string>>(request);
        }

        public Task<IEnumerable<string>> AutocompleteAsync(string postcode, int? limit = null)
        {
            var request = CreateAutocompleteRequest(postcode, limit);
            return ExecuteAsync<List<string>>(request).ContinueWith(t => t.Result as IEnumerable<string>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public PostcodeResult Random()
        {
            var request = CreateRandomRequest();
            return Execute<PostcodeResult>(request);
        }

        public Task<PostcodeResult> RandomAsync()
        {
            var request = CreateRandomRequest();
            return ExecuteAsync<PostcodeResult>(request);
        }

        public OutwardCodeResult OutwardCodeLookup(string outcode)
        {
            var request = CreateOutwardCodeLookupRequest(outcode);
            return Execute<OutwardCodeResult>(request);
        }

        // TODO: outward lookup async
       
        // TODO: outcode reverse geocoding (& async)

        // TODO: nearest outcode (& async)

        public TerminatedPostcodeResult Terminated(string postcode)
        {
            var request = CreateTerminatedRequest(postcode);
            return Execute<TerminatedPostcodeResult>(request);
        }

        public Task<TerminatedPostcodeResult> TerminatedAsync(string postcode)
        {
            var request = CreateTerminatedRequest(postcode);
            return ExecuteAsync<TerminatedPostcodeResult>(request);
        }

        public PlaceResult PlaceLookup(string code)
        {
            var request = CreatePlaceLookupRequest(code);
            return Execute<PlaceResult>(request);
        }
        
        public Task<PlaceResult> PlaceLookupAsync(string code)
        {
            var request = CreatePlaceLookupRequest(code);
            return ExecuteAsync<PlaceResult>(request);
        }

        public IEnumerable<PlaceResult> PlaceQuery(string q, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceResult>> PlaceQueryAsync(string q, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public PlaceResult RandomPlace()
        {
            throw new NotImplementedException();
        }

        public Task<PlaceResult> RandomPlaceAsync()
        {
            throw new NotImplementedException();
        }


        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_endpoint) };
            var response = client.Execute<RawResult<T>>(request);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
        }

        private async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_endpoint) };
            var response = await client.ExecuteTaskAsync<RawResult<T>>(request).ConfigureAwait(false);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
        }

        private static RestRequest CreateLookupRequest(string postcode)
        {
            return new RestRequest(string.Format("postcodes/{0}", postcode), Method.GET);
        }

        private static RestRequest CreateBulkLookupRequest(IEnumerable<string> postcodes)
        {
            var request = new RestRequest("postcodes", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { postcodes });
            return request;
        }

        private static RestRequest CreateReverseGeocodeRequest(ReverseGeocodeQuery query)
        {
            var request = new RestRequest("postcodes", Method.GET);
            request.AddParameter("lon", query.Longitude);
            request.AddParameter("lat", query.Latitude);
            if (query.Limit.HasValue) request.AddParameter("limit", query.Limit);
            if (query.Radius.HasValue) request.AddParameter("radius", query.Radius);
            if (query.WideSearch.HasValue) request.AddParameter("wideSearch", query.WideSearch);
            return request;
        }

        private static RestRequest CreateBulkReverseGeocodeRequest(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = new RestRequest("postcodes", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonDotNetSerializer()
            };
            request.AddJsonBody(new { geolocations = queries });
            return request;
        }

        private static RestRequest CreateQueryRequest(string q, int? limit)
        {
            var request = new RestRequest("postcodes", Method.GET);
            request.AddQueryParameter("q", q);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return request;
        }

        private static RestRequest CreateValidateRequest(string postcode)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/validate", postcode), Method.GET);
            return request;
        }

        private static RestRequest CreateNearestRequest(string postcode, int? limit, int? radius)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/nearest", postcode), Method.GET);
            if (limit.HasValue) request.AddParameter("limit", limit);
            if (radius.HasValue) request.AddParameter("radius", radius);
            return request;
        }

        private static RestRequest CreateAutocompleteRequest(string postcode, int? limit)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/autocomplete", postcode), Method.GET);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return request;
        }

        private static RestRequest CreateRandomRequest()
        {
            var request = new RestRequest("random/postcodes", Method.GET);
            return request;
        }

        private static RestRequest CreateOutwardCodeLookupRequest(string outcode)
        {
            return new RestRequest(string.Format("outcodes/{0}", outcode), Method.GET);
        }

        private static RestRequest CreateTerminatedRequest(string postcode)
        {
            return new RestRequest(string.Format("terminated_postcodes/{0}", postcode), Method.GET);
        }

        private static RestRequest CreatePlaceLookupRequest(string code)
        {
            var request = new RestRequest(string.Format("places/{0}", code), Method.GET);
            return request;
        }
    }
}