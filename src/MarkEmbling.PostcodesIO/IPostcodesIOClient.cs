using MarkEmbling.PostcodesIO.Data;
using MarkEmbling.PostcodesIO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO
{
    public interface IPostcodesIOClient {
        // TODO: documentation

        PostcodeData Lookup(string postcode);
        Task<PostcodeData> LookupAsync(string postcode);
        IEnumerable<BulkQueryResult<string, PostcodeData>> BulkLookup(IEnumerable<string> postcodes);
        Task<IEnumerable<BulkQueryResult<string, PostcodeData>>> BulkLookupAsync(IEnumerable<string> postcodes);
        IEnumerable<PostcodeData> Query(string q, int? limit = null);
        Task<IEnumerable<PostcodeData>> QueryAsync(string q, int? limit = null);
        bool Validate(string postcode);
        Task<bool> ValidateAsync(string postcode);

        IEnumerable<PostcodeData> LookupLatLon(ReverseGeocodeQuery query);
        Task<IEnumerable<PostcodeData>> LookupLatLonAsync(ReverseGeocodeQuery query);
        IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeData>>> BulkLookupLatLon(IEnumerable<ReverseGeocodeQuery> queries);
        Task<IEnumerable<BulkQueryResult<ReverseGeocodeQuery, List<PostcodeData>>>> BulkLookupLatLonAsync(IEnumerable<ReverseGeocodeQuery> queries);

        IEnumerable<string> Autocomplete(string postcode, int? limit = null);
        Task<IEnumerable<string>> AutocompleteAsync(string postcode, int? limit = null);
        PostcodeData Random();
        Task<PostcodeData> RandomAsync();

        IEnumerable<NearestResult> Nearest(string postcode, int? limit = null, int? radius = null);
        Task<IEnumerable<NearestResult>> NearestAsync(string postcode, int? limit = null, int? radius = null);

        OutcodeData OutwardCodeLookup(string outcode);

        TerminatedPostcodeData Terminated(string postcode);
        Task<TerminatedPostcodeData> TerminatedAsync(string postcode);

        /*
        IEnumerable<OutwardCodeResult> OutwardCodeLookupLatLon(ReverseGeocodeQuery query);
        IEnumerable<PostcodeResult> NearestOutwardCode(string outcode, int? limit = null, int? radius = null);
         */
    }
}