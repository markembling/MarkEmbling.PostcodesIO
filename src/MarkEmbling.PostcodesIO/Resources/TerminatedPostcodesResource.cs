using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Resources
{
    public class TerminatedPostcodesResource
    {
        private readonly IRequestExecutor _requestExecutor;

        public TerminatedPostcodesResource(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor;
        }

        public TerminatedPostcodeResult Lookup(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return _requestExecutor.ExecuteRequest<TerminatedPostcodeResult>(request);
        }

        public Task<TerminatedPostcodeResult> LookupAsync(string postcode)
        {
            var request = CreateLookupRequest(postcode);
            return _requestExecutor.ExecuteRequestAsync<TerminatedPostcodeResult>(request);
        }

        private static RestRequest CreateLookupRequest(string postcode)
        {
            return new RestRequest(string.Format("terminated_postcodes/{0}", postcode), Method.GET, DataFormat.Json);
        }
    }
}
