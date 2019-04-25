using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace MarkEmbling.PostcodesIO.Internals
{
    public class JsonDotNetSerializer : ISerializer, IDeserializer {
        public JsonDotNetSerializer() {
            ContentType = "application/json";
        }

        public string Serialize(object obj) {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ContractResolver = new LowercaseWithUnderscoresContractResolver()
                });
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content,
                new JsonSerializerSettings
                {
                    ContractResolver = new LowercaseWithUnderscoresContractResolver()
                });
        }

        public string ContentType { get; set; }
    }
}