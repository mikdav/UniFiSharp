using UniFiSharp.Json;

namespace UniFiSharp.Orchestration.Devices
{
    public class UnknownInfrastructureNetworkedDevice : IInfrastructureNetworkedDevice
    {
        public NetworkDevice Details => Json;

        public UnknownInfrastructureNetworkedDevice(UniFiNetworkApi api, NetworkDevice json) : base(api, json) { }
    }
}
