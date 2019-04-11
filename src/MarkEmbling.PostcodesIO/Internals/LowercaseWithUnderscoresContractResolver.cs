using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace MarkEmbling.PostcodesIO.Internals {
    public class LowercaseWithUnderscoresContractResolver : DefaultContractResolver {
        protected override string ResolvePropertyName(string propertyName) {
            var name = Regex.Replace(propertyName, "([A-Z])", "_$1").ToLowerInvariant();
            if (name.StartsWith("_")) name = name.Substring(1);
            return name;
        }
    }
}