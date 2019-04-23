namespace MarkEmbling.PostcodesIO
{
    public interface IPostcodesIOClient {
        PostcodesResource Postcodes { get; }
        TerminatedPostcodesResource TerminatedPostcodes { get; }
        OutwardCodesResource OutwardCodes { get; }
        PlacesResource Places { get; }
    }
}