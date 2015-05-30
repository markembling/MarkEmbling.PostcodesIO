using System;
using RestSharp;

namespace MarkEmbling.PostcodesIO {
    public class PostcodesIOApi {
        private readonly string _endpoint;

        public PostcodesIOApi(string endpoint) {
            _endpoint = endpoint;
        }

        public T Execute<T>(RestRequest request) where T : new() {
            var client = new RestClient {BaseUrl = new Uri(_endpoint)};
            var response = client.Execute<T>(request);

            if (response.ErrorException != null) {
                throw new PostcodesIOException(response.ErrorException);
            }

            return response.Data;
        }
    }
}