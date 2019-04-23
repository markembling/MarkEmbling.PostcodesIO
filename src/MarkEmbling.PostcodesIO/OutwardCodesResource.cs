using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO
{
    public class OutwardCodesResource
    {
        private readonly IRequestExecutor _requestExecutor;

        public OutwardCodesResource(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor;
        }

        public OutwardCodeResult Lookup(string outcode)
        {
            var request = CreateLookupRequest(outcode);
            return _requestExecutor.ExecuteRequest<OutwardCodeResult>(request);
        }

        public Task<OutwardCodeResult> LookupAsync(string outcode)
        {
            var request = CreateLookupRequest(outcode);
            return _requestExecutor.ExecuteRequestAsync<OutwardCodeResult>(request);
        }

        public IEnumerable<OutwardCodeResult> ReverseGeocode(double latitude, double longitude, int? limit = null, int? radius = null)
        {
            var request = CreateReverseGeocodeRequest(latitude, longitude, limit, radius);
            return _requestExecutor.ExecuteRequest<List<OutwardCodeResult>>(request);
        }

        public Task<IEnumerable<OutwardCodeResult>> ReverseGeocodeAsync(double latitude, double longitude, int? limit = null, int? radius = null)
        {
            var request = CreateReverseGeocodeRequest(latitude, longitude, limit, radius);
            return _requestExecutor.ExecuteRequestAsync<List<OutwardCodeResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<OutwardCodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public IEnumerable<OutwardCodeResult> Nearest(string outcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(outcode, limit, radius);
            return _requestExecutor.ExecuteRequest<List<OutwardCodeResult>>(request);
        }

        public Task<IEnumerable<OutwardCodeResult>> NearestAsync(string outcode, int? limit = null, int? radius = null)
        {
            var request = CreateNearestRequest(outcode, limit, radius);
            return _requestExecutor.ExecuteRequestAsync<List<OutwardCodeResult>>(request)
                .ContinueWith(t => t.Result as IEnumerable<OutwardCodeResult>, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private static RestRequest CreateLookupRequest(string outcode)
        {
            return new RestRequest(string.Format("outcodes/{0}", outcode), Method.GET);
        }

        private static RestRequest CreateReverseGeocodeRequest(double latitude, double longitude, int? limit, int? radius)
        {
            var request = new RestRequest("outcodes", Method.GET);
            request.AddParameter("lon", longitude);
            request.AddParameter("lat", latitude);
            if (limit.HasValue) request.AddParameter("limit", limit);
            if (radius.HasValue) request.AddParameter("radius", radius);
            return request;
        }

        private static RestRequest CreateNearestRequest(string outcode, int? limit, int? radius)
        {
            var request = new RestRequest(string.Format("outcodes/{0}/nearest", outcode), Method.GET);
            if (limit.HasValue) request.AddParameter("limit", limit);
            if (radius.HasValue) request.AddParameter("radius", radius);
            return request;
        }
    }
}
