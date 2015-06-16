using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Serializers;

namespace MarkEmbling.PostcodesIO {
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

    public class LowercaseWithUnderscoresContractResolver : DefaultContractResolver {
        protected override string ResolvePropertyName(string propertyName) {
            var name = Regex.Replace(propertyName, "([A-Z])", "_$1").ToLowerInvariant();
            if (name.StartsWith("_")) name = name.Substring(1);
            return name;
        }
    }
}