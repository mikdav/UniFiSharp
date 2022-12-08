using Newtonsoft.Json;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    public class ResourceLink
    {
        [JsonProperty("resource_type")]
        public string Type { get; set; }

        [JsonProperty("resource_value")]
        public string Value { get; set; }
    }
}
