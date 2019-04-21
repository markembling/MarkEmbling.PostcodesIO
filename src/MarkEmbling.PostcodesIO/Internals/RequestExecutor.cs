using MarkEmbling.PostcodesIO.Exceptions;
using MarkEmbling.PostcodesIO.Results;
using RestSharp;
using System.Collections;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Internals
{
    public interface IRequestExecutor
    {
        /// <summary>
        /// Execute a request and return the result
        /// </summary>
        /// <typeparam name="T">Type to deserialise the result into</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns>Result object</returns>
        T ExecuteRequest<T>(RestRequest request) where T : new();

        /// <summary>
        /// Execute a request asyncronously and return the result
        /// </summary>
        /// <typeparam name="T">Type to deserialise the result into</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns>Result object</returns>
        Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : new();
    }

    /// <summary>
    /// Takes a request and executes it using a client, processing and returning the results
    /// </summary>
    public class RequestExecutor : IRequestExecutor
    {
        private readonly IRestClient _restClient;

        public RequestExecutor(string endpoint)
        {
            _restClient = new RestClient(endpoint);
        }

        public RequestExecutor(IRestClient restClient)
        {
            _restClient = restClient;
        }

        /// <summary>
        /// Execute a request and return the result
        /// </summary>
        /// <typeparam name="T">Type to deserialise the result into</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns>Result object</returns>
        public T ExecuteRequest<T>(RestRequest request) where T : new()
        {
            var response = _restClient.Execute<RawResult<T>>(request);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return NormaliseResults(response.Data.Result);
        }

        /// <summary>
        /// Execute a request asyncronously and return the result
        /// </summary>
        /// <typeparam name="T">Type to deserialise the result into</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns>Result object</returns>
        public async Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : new()
        {
            var response = await _restClient.ExecuteTaskAsync<RawResult<T>>(request).ConfigureAwait(false);

            if (response.ErrorException != null)
                throw new PostcodesIOApiException(response.ErrorException);
            if (response.Data == null)
                throw new PostcodesIOEmptyResponseException(response.StatusCode);

            return NormaliseResults(response.Data.Result);
        }

        /// <summary>
        /// Checks to see if the type we're dealing with here is a collection. If it is, and the result
        /// we've been given is null, we'll instead return a new empty collection. In all other cases,
        /// we'll just pass through the result untouched.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="result">Result</param>
        /// <returns>Result, appropriately normalised</returns>
        private T NormaliseResults<T>(T result) where T : new()
        {
            if (typeof(ICollection).IsAssignableFrom(typeof(T)))
            {
                if (result == null) return new T();
            }
            return result;
        }
    }
}
