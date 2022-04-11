using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniFiSharp.Json
{
    public class AccessDevice
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("default")]
        public bool defaultSettings { get; set; }

        [JsonProperty("device_id")]
        public string device_id { get; set; }

        [JsonProperty("ip")]
        public string ip { get; set; }

        [JsonProperty("known_cfgversion")]
        public string known_cfgversion { get; set; }

        [JsonProperty("mac")]
        public string mac { get; set; }

        [JsonProperty("model")]
        public string model { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("serial")]
        public string serial { get; set; }

        [JsonProperty("site_id")]
        public string site_id { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("sys_stats")]
        public string sys_stats { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("version")]
        public string version { get; set; }
    }
}
