using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Internals
{
    public interface IRequestExecutor
    {
        T ExecuteRequest<T>(RestRequest request) where T : new();
        Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : new();
    }

    /// <summary>
    /// Takes a request and executes it using a client, processing and returning the results
    /// </summary>
    public class RequestExecutor : IRequestExecutor
    {
        private readonly string _endpoint;

        public RequestExecutor(string endpoint)
        {
            _endpoint = endpoint;
        }

        public T ExecuteRequest<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_endpoint) };
            var response = client.Execute<RawResult<T>>(request);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
            //return NormaliseResults(response.Data.Result);
        }

        public async Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_endpoint) };
            var response = await client.ExecuteTaskAsync<RawResult<T>>(request).ConfigureAwait(false);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return response.Data.Result;
            //return NormaliseResults(response.Data.Result);
        }

        ///// <summary>
        ///// Checks to see if the type we're dealing with here is a collection. If it is, and the result
        ///// we've been given is null, we'll instead return a new empty collection. In all other cases,
        ///// we'll just pass through the result untouched.
        ///// </summary>
        ///// <typeparam name="T">Result type</typeparam>
        ///// <param name="result">Result</param>
        ///// <returns>Result, appropriately normalised</returns>
        //private T NormaliseResults<T>(T result) where T : new()
        //{
        //    if (typeof(T).IsAssignableFrom(typeof(ICollection)))
        //    {
        //        if (result == null) return new T();
        //    }
        //    return result;
        //}
    }
}
