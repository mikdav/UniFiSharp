using Newtonsoft.Json;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    public class DeviceConfig
    {
        public string Device_Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Tag { get; set; }
    }
}
