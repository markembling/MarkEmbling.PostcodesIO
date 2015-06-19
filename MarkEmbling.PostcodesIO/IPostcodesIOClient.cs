using System.Collections.Generic;
using MarkEmbling.PostcodesIO.Results;

namespace MarkEmbling.PostcodesIO {
    public interface IPostcodesIOClient {
        // TODO: documentation

        PostcodeLookupResult Lookup(string postcode);
        IEnumerable<BulkQueryResult<string, PostcodeLookupResult>> BulkLookup(IEnumerable<string> postcodes);
        IEnumerable<PostcodeLookupResult> Query(string q, int? limit = null);
        bool Validate(string postcode);

        IEnumerable<PostcodeLookupResult> LookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeLookupResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);

        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        PostcodeLookupResult Random();

        

        /*
        object Nearest(string postcode, int? limit = null, int? radius = null);
        OutwardCodeLookupResult LookupOutwardCode(string outcode);
         */
    }
}