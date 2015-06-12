namespace MarkEmbling.PostcodesIO.Results {
    public class QueryResult<TQuery, TResult> where TResult : class {
        public TQuery Query { get; set; }
        public TResult Result { get; set; }
    }
}