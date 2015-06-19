using System;
using System.Collections;
using System.Collections.Generic;
using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;

namespace MarkEmbling.PostcodesIO {
    public class PostcodesIOClient : IPostcodesIOClient {
        private readonly string _endpoint;

        public PostcodesIOClient(string endpoint = "https://api.postcodes.io") {
            _endpoint = endpoint;
        }

        public T Execute<T>(RestRequest request) where T : new() {
            var client = new RestClient {BaseUrl = new Uri(_endpoint)};
            var response = client.Execute<RawResult<T>>(request);

            if (response.ErrorException != null) 
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null) 
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
        }

        public PostcodeLookupResult Lookup(string postcode) {
            var request = new RestRequest(string.Format("postcodes/{0}", postcode), Method.GET);
            return Execute<PostcodeLookupResult>(request);
        }

        public IEnumerable<BulkQueryResult<string, PostcodeLookupResult>> BulkLookup(IEnumerable<string> postcodes) {
            var request = new RestRequest("postcodes", Method.POST) {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new {postcodes});
            return Execute<List<BulkQueryResult<string, PostcodeLookupResult>>>(request);
        }

        public IEnumerable<PostcodeLookupResult> Query(string q, int? limit = null) {
            var request = new RestRequest("postcodes", Method.GET);
            request.AddQueryParameter("q", q);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return Execute<List<PostcodeLookupResult>>(request);
        }

        public bool Validate(string postcode) {
            var request = new RestRequest(string.Format("postcodes/{0}/validate", postcode), Method.GET);
            return Execute<bool>(request);
        }

        public IEnumerable<PostcodeLookupResult> LookupLatLon(ReverseGeocodeQuery query) {
            var request = new RestRequest("postcodes", Method.GET);
            request.AddParameter("lat", query.Latitude);
            request.AddParameter("lon", query.Longitude);
            if (query.Limit.HasValue) request.AddParameter("limit", query.Limit);
            return Execute<List<PostcodeLookupResult>>(request);
        }

        public IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeLookupResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries) {
            var request = new RestRequest("postcodes", Method.POST) {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonDotNetSerializer()
            };
            request.AddBody(new {geolocations = queries});
            return Execute<List<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeLookupResult>>>>(request);
        }

        public IEnumerable<string> Autocomplete(string postcode, int? limit = null) {
            var request = new RestRequest(string.Format("postcodes/{0}/autocomplete", postcode), Method.GET);
            if (limit.HasValue) request.AddParameter("limit", limit);
            return Execute<List<string>>(request);
        }

        public PostcodeLookupResult Random() {
            var request = new RestRequest("random/postcodes", Method.GET);
            return Execute<PostcodeLookupResult>(request);
        }
    }
}