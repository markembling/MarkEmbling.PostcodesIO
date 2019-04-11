using System;

namespace MarkEmbling.PostcodesIO.Results {
    /// <summary>
    /// A single query/result pair from a bulk API call
    /// </summary>
    /// <typeparam name="TQuery">Type of the query</typeparam>
    /// <typeparam name="TResult">Result of the query</typeparam>
    [Serializable]
    public class BulkQueryResult<TQuery, TResult> where TResult : class {
        public TQuery Query { get; set; }
        public TResult Result { get; set; }
    }
}