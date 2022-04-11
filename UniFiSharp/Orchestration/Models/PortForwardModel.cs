using UniFiSharp.Json;

namespace UniFiSharp.Orchestration.Models
{
    public class PortForwardModel
    {
        public string PortForwardId => Json._id;
        public string Source => Json.src;
        public string Destination => Json.fwd;
        public int FromPort => Json.fwd_port;
        public string Proto => Json.proto;
        public int DestinationPort => Json.dst_port;
        public string Name => Json.name;

        private PortForward Json { get; set; }
        public PortForwardModel(PortForward json)
        {
            Json = json;
        }

        public static PortForwardModel CreateFromJson(PortForward json)
        {
            return new PortForwardModel(json);
        }
    }
}
