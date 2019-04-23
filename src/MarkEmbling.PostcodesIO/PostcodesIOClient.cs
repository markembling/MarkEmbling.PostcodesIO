using MarkEmbling.PostcodesIO.Internals;

namespace MarkEmbling.PostcodesIO
{
    public class PostcodesIOClient : IPostcodesIOClient
    {
        private readonly IRequestExecutor _requestExecutor;

        public PostcodesIOClient(string endpoint = "https://api.postcodes.io")
        {
            _requestExecutor = new RequestExecutor(endpoint);

            Postcodes = new PostcodesResource(_requestExecutor);
            TerminatedPostcodes = new TerminatedPostcodesResource(_requestExecutor);
            OutwardCodes = new OutwardCodesResource(_requestExecutor);
            Places = new PlacesResource(_requestExecutor);
        }

        public PostcodesResource Postcodes { get; private set; }
        public TerminatedPostcodesResource TerminatedPostcodes { get; private set; }
        public OutwardCodesResource OutwardCodes { get; private set; }
        public PlacesResource Places { get; private set; }
    }
}