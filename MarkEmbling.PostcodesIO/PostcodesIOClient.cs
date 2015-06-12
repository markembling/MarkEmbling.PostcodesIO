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
            var response = client.Execute<T>(request);

            if (response.ErrorException != null) {
                throw new PostcodesIOException(response.ErrorException);
            }

            return response.Data;
        }

        public PostcodeLookupResult Lookup(string postcode) {
            var request = new RestRequest(string.Format("postcodes/{0}", postcode), Method.GET) {
                RootElement = "result"
            };
            return Execute<PostcodeLookupResult>(request);
        }

        public IEnumerable<QueryResult<string, PostcodeLookupResult>> BulkLookup(IEnumerable<string> postcodes) {
            var request = new RestRequest("postcodes", Method.POST) {
                RootElement = "result",
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new {postcodes});
            return Execute<List<QueryResult<string, PostcodeLookupResult>>>(request);
        }

        public LatLonLookupResult LookupLatLon(ReverseGeocodeQuery query) {
            throw new NotImplementedException();
        }

        public LatLonBulkLookupResult BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries) {
            throw new NotImplementedException();
        }
    }

    public interface IPostcodesIOClient {
        PostcodeLookupResult Lookup(string postcode);
        IEnumerable<QueryResult<string, PostcodeLookupResult>> BulkLookup(IEnumerable<string> postcodes);

        LatLonLookupResult LookupLatLon(ReverseGeocodeQuery query);
        LatLonBulkLookupResult BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);

        /*
        object Query(string q, int? limit = null);
        bool Validate(string postcode);
        object Nearest(string postcode, int? limit = null, int? radius = null);
        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        PostcodeLookupResult Random();
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