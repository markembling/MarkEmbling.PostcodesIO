using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;

namespace MarkEmbling.PostcodesIO
{
    public class PostcodesIOClient : IPostcodesIOClient
    {
        public PostcodesIOClient(string endpoint = "https://api.postcodes.io")
            : this(new RequestExecutor(endpoint))
        { }

        public PostcodesIOClient(IRequestExecutor requestExecutor)
        {
            Postcodes = new PostcodesResource(requestExecutor);
            TerminatedPostcodes = new TerminatedPostcodesResource(requestExecutor);
            OutwardCodes = new OutwardCodesResource(requestExecutor);
            Places = new PlacesResource(requestExecutor);
        }

        public PostcodesResource Postcodes { get; private set; }
        public TerminatedPostcodesResource TerminatedPostcodes { get; private set; }
        public OutwardCodesResource OutwardCodes { get; private set; }
        public PlacesResource Places { get; private set; }
    }
}