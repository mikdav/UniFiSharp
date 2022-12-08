using Newtonsoft.Json;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    internal class NfcsSessionRequest
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("has_lock")]
        public bool HasLock { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
