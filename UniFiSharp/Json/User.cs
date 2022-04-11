﻿using Newtonsoft.Json;

namespace UniFiSharp.Json
{
    public class User
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("duration")]
        public int duration { get; set; }
        [JsonProperty("first_seen")]
        public int first_seen { get; set; }
        [JsonProperty("hostname")]
        public string hostname { get; set; }
        [JsonProperty("is_guest")]
        public bool is_guest { get; set; }
        [JsonProperty("is_wired")]
        public bool is_wired { get; set; }
        [JsonProperty("last_seen")]
        public int last_seen { get; set; }
        [JsonProperty("mac")]
        public string mac { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("noted")]
        public bool noted { get; set; }
        [JsonProperty("oui")]
        public string oui { get; set; }
        [JsonProperty("rx_bytes")]
        public long rx_bytes { get; set; }
        [JsonProperty("rx_packets")]
        public long rx_packets { get; set; }
        [JsonProperty("site_id")]
        public string site_id { get; set; }
        [JsonProperty("tx_bytes")]
        public long tx_bytes { get; set; }
        [JsonProperty("tx_packets")]
        public long tx_packets { get; set; }
        [JsonProperty("usergroup_id")]
        public string usergroup_id { get; set; }
    }
}
