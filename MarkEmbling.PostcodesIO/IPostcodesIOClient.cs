using System.Collections.Generic;
using MarkEmbling.PostcodesIO.Results;

namespace MarkEmbling.PostcodesIO {
    public interface IPostcodesIOClient {
        // TODO: documentation

        PostcodeResult Lookup(string postcode);
        IEnumerable<BulkQueryResult<string, PostcodeResult>> BulkLookup(IEnumerable<string> postcodes);
        IEnumerable<PostcodeResult> Query(string q, int? limit = null);
        bool Validate(string postcode);

        IEnumerable<PostcodeResult> LookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);

        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        PostcodeResult Random();



        /*
        IEnumerable<PostcodeResult> Nearest(string postcode, int? limit = null, int? radius = null);
        
        OutwardCodeResult OutwardCodeLookup(string outcode);
        IEnumerable<OutwardCodeResult> OutwardCodeLookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<PostcodeResult> NearestOutwardCode(string outcode, int? limit = null, int? radius = null);
         */
    }
}