using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO
{
    public class PostcodesIOClient : IPostcodesIOClient {
        private readonly string _endpoint;
        private readonly string _proxyServerUrl;
        private readonly RestClient _client;

        public PostcodesIOClient(string endpoint = "https://api.postcodes.io", string proxyServerUrl = null) {
            _endpoint = endpoint;
            _proxyServerUrl = proxyServerUrl;

            var clientOptions = new RestClientOptions(_endpoint);
            if (!string.IsNullOrEmpty(_proxyServerUrl))
            {
                clientOptions.Proxy = new WebProxy(_proxyServerUrl, true)
                {
                    Credentials = CredentialCache.DefaultCredentials
                };
            }
            _client = new RestClient(
                baseUrl: new Uri(_endpoint),
                configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower })
            );
        }

        private T Execute<T>(RestRequest request) where T : new() {
            var response = _client.Execute<RawResult<T>>(request);

            if (response.ErrorException != null) 
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null) 
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
        }

        private async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteAsync<RawResult<T>>(request).ConfigureAwait(false);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
        }

        public PostcodeResult Lookup(string postcode) {
            var request = CreateLookupRequest(postcode);
            return Execute<PostcodeResult>(request);
        }

        public OutwardCodeResult OutwardCodeLookup(string outcode) {
            var request = CreateOutwardCodeLookupRequest(outcode);
            return Execute<OutwardCodeResult>(request);
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

        public IEnumerable<PostcodeResult> LookupLatLon(ReverseGeocodeQuery query)
        {
            var request = CreateLookupLocationRequest(query);
            return Execute<List<PostcodeResult>>(request);
        }

        public Task<IEnumerable<PostcodeResult>> LookupLatLonAsync(ReverseGeocodeQuery query)
        {
            var request = CreateLookupLocationRequest(query);
            return ExecuteAsync<List<PostcodeResult>>(request).ContinueWith(t => t.Result as IEnumerable<PostcodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkLookupLatLon(queries);
            return Execute<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request);
        }

        public Task<IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>> BulkLookupLatLonAsync(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = CreateBulkLookupLatLon(queries);
            return ExecuteAsync<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>>(request).ContinueWith(t => t.Result as IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>, TaskContinuationOptions.OnlyOnRanToCompletion);
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

        public IEnumerable<NearestResult> Nearest(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearest(postcode, limit, radius);
            return Execute<List<NearestResult>>(request);
        }

        public Task<IEnumerable<NearestResult>> NearestAsync(string postcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearest(postcode, limit, radius);
            return ExecuteAsync<List<NearestResult>>(request).ContinueWith(t => t.Result as IEnumerable<NearestResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

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

        private static RestRequest CreateBulkLookupRequest(IEnumerable<string> postcodes)
        {
            var request = new RestRequest("postcodes", Method.Post)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { postcodes });
            return request;
        }

        private static RestRequest CreateQueryRequest(string q, int? limit)
        {
            var request = new RestRequest("postcodes", Method.Get);
            request.AddQueryParameter("q", q);
            if (limit.HasValue) request.AddParameter("limit", limit.Value);
            return request;
        }

        private static RestRequest CreateValidateRequest(string postcode)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/validate", postcode), Method.Get);
            return request;
        }

        private static RestRequest CreateLookupLocationRequest(ReverseGeocodeQuery query)
        {
            var request = new RestRequest("postcodes", Method.Get);
            request.AddParameter("lat", query.Latitude);
            request.AddParameter("lon", query.Longitude);
            if (query.Limit.HasValue) request.AddParameter("limit", query.Limit.Value);
            return request;
        }

        private static RestRequest CreateLookupRequest(string postcode)
        {
            return new RestRequest(string.Format("postcodes/{0}", postcode), Method.Get);
        }

        private static RestRequest CreateOutwardCodeLookupRequest(string outcode)
        {
            return new RestRequest(string.Format("outcodes/{0}", outcode), Method.Get);
        }

        private static RestRequest CreateBulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries)
        {
            var request = new RestRequest("postcodes", Method.Post);
            request.AddJsonBody(new { geolocations = queries });
            return request;
        }

        private static RestRequest CreateAutocompleteRequest(string postcode, int? limit)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/autocomplete", postcode), Method.Get);
            if (limit.HasValue) request.AddParameter("limit", limit.Value);
            return request;
        }

        private static RestRequest CreateRandomRequest()
        {
            var request = new RestRequest("random/postcodes", Method.Get);
            return request;
        }

        private static RestRequest CreateNearest(string postcode, int? limit, int? radius)
        {
            var request = new RestRequest(string.Format("postcodes/{0}/nearest", postcode), Method.Get);
            if (limit.HasValue) request.AddParameter("limit", limit.Value);
            if (radius.HasValue) request.AddParameter("radius", radius.Value);
            return request;
        }

        private static RestRequest CreateTerminatedRequest(string postcode)
        {
            return new RestRequest(string.Format("terminated_postcodes/{0}", postcode), Method.Get);
        }
    }
}