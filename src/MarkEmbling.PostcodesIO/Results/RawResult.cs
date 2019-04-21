namespace MarkEmbling.PostcodesIO.Results
{
    /// <summary>
    /// Raw result returned from the Postcodes.io API
    /// </summary>
    /// <typeparam name="T">Type to deserialise "result"</typeparam>
    public class RawResult<T> {
        public virtual int Status { get; set; }
        public virtual T Result { get; set; }
    }
}