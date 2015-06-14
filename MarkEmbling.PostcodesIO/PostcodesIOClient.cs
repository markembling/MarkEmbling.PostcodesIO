using System;
using System.Collections;
using System.Collections.Generic;
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

            if (response.ErrorException != null) {
                throw new PostcodesIOException(response.ErrorException);
            }

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

        public IEnumerable<PostcodeLookupResult> LookupLatLon(ReverseGeocodeQuery query) {
            var request = new RestRequest("postcodes", Method.GET);
            request.AddParameter("lat", query.Latitude);
            request.AddParameter("lon", query.Longitude);
            if (query.Limit.HasValue) request.AddParameter("limit", query.Limit);
            return Execute<List<PostcodeLookupResult>>(request);
        }

        public IEnumerable<BulkQueryResult<ReverseGeocodeQuery, IEnumerable<PostcodeLookupResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries) {
            throw new NotImplementedException();
        }

        public bool Validate(string postcode) {
            var request = new RestRequest(string.Format("postcodes/{0}/validate", postcode), Method.GET);
            return Execute<bool>(request);
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

    public interface IPostcodesIOClient {
        // TODO: documentation

        PostcodeLookupResult Lookup(string postcode);
        IEnumerable<BulkQueryResult<string, PostcodeLookupResult>> BulkLookup(IEnumerable<string> postcodes);

        IEnumerable<PostcodeLookupResult> LookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<BulkQueryResult<ReverseGeocodeQuery, IEnumerable<PostcodeLookupResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);

        bool Validate(string postcode);
        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        PostcodeLookupResult Random();

        /*
        object Query(string q, int? limit = null);
        object Nearest(string postcode, int? limit = null, int? radius = null);
        OutwardCodeLookupResult LookupOutwardCode(string outcode);
         */
    }

    public class ReverseGeocodeQuery {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int? Limit { get; set; }
        public int? Radius { get; set; }
        public bool? WideSearch { get; set; }
    }
}