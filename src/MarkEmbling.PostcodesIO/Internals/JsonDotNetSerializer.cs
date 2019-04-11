using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MarkEmbling.PostcodesIO.Internals {
    public class JsonDotNetSerializer : ISerializer {
        public JsonDotNetSerializer() {
            ContentType = "application/json";
        }

        public string Serialize(object obj) {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ContractResolver = new LowercaseWithUnderscoresContractResolver()
                });
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}