namespace MarkEmbling.PostcodesIO.Results
{
    /// <summary>
    /// Raw result object returned from the Postcodes.io API
    /// </summary>
    /// <typeparam name="T">Type to deserialise "result"</typeparam>
    public class RawResult<T> {
        public int Status { get; set; }
        public T Result { get; set; }
    }
}