using Newtonsoft.Json;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    public class AccessCard
    {
        [JsonProperty("force_add_nfc")]
        public bool ForceAdd { get; set; }

        [JsonProperty("nfc_token")]
        public string Token { get; set; }
    }
}
