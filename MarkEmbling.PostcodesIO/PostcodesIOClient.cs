using System.Collections;
using System.Collections.Generic;

namespace MarkEmbling.PostcodesIO {
    public class PostcodesIOClient : IPostcodesIOClient {
        public PostcodesIOClient() {
            
        }

        public PostcodeLookupResult Lookup(string postcode) {
            throw new System.NotImplementedException();
        }

        public PostcodeBulkLookupResult BulkLookup(IEnumerable<string> postcodes) {
            throw new System.NotImplementedException();
        }

        public LatLonLookupResult LookupLatLon(ReverseGeocodeQuery query) {
            throw new System.NotImplementedException();
        }

        public LatLonBulkLookupResult BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries) {
            throw new System.NotImplementedException();
        }
    }

    public interface IPostcodesIOClient {
        PostcodeLookupResult Lookup(string postcode);
        PostcodeBulkLookupResult BulkLookup(IEnumerable<string> postcodes);

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