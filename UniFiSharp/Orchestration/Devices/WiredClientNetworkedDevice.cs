﻿using UniFiSharp.Json;

namespace UniFiSharp.Orchestration.Devices
{
    public class WiredClientNetworkedDevice : IClientNetworkedDevice
    {
        public string SwitchMac => Json.sw_mac;
        public int SwitchPort => Json.sw_port;
        public int SwitchDepth => Json.sw_depth;

        public WiredClientNetworkedDevice(UniFiNetworkApi api, Client json) : base(api, json) { }
    }
}
