using System.Collections.Generic;
using System.Threading.Tasks;
using MarkEmbling.PostcodesIO.Results;

namespace MarkEmbling.PostcodesIO {
    public interface IPostcodesIOClient {
        // TODO: documentation

        PostcodeResult Lookup(string postcode);
        Task<PostcodeResult> LookupAsync(string postcode);
        IEnumerable<BulkQueryResult<string, PostcodeResult>> BulkLookup(IEnumerable<string> postcodes);
        Task<IEnumerable<BulkQueryResult<string, PostcodeResult>>> BulkLookupAsync(IEnumerable<string> postcodes);
        IEnumerable<PostcodeResult> Query(string q, int? limit = null);
        Task<IEnumerable<PostcodeResult>> QueryAsync(string q, int? limit = null);
        bool Validate(string postcode);
        Task<bool> ValidateAsync(string postcode);

        IEnumerable<PostcodeResult> LookupLatLon(ReverseGeocodeQuery query);
        Task<IEnumerable<PostcodeResult>> LookupLatLonAsync(ReverseGeocodeQuery query);
        IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);
        Task<IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeResult>>>> BulkLookupLatLonAsync(IEnumerable<ReverseGeocodeQuery> queries);

        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        Task<IEnumerable<string>> AutocompleteAsync(string postcode, int? limit = null);
        PostcodeResult Random();
        Task<PostcodeResult> RandomAsync();

        IEnumerable<PostcodeResult> Nearest(string postcode, int? limit = null, int? radius = null);
        Task<IEnumerable<PostcodeResult>> NearestAsync(string postcode, int? limit = null, int? radius = null);

        OutwardCodeResult OutwardCodeLookup(string outcode);

        TerminatedPostcodeResult Terminated(string postcode);
        Task<TerminatedPostcodeResult> TerminatedAsync(string postcode);

        /*
        IEnumerable<OutwardCodeResult> OutwardCodeLookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<PostcodeResult> NearestOutwardCode(string outcode, int? limit = null, int? radius = null);
         */
    }
}