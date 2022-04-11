namespace UniFiSharp.Orchestration.Devices
{
    public abstract class INetworkedDevice
    {
        public abstract string Id { get; }
        public abstract string Name { get; }

        protected UniFiNetworkApi API { get; set; }

        public INetworkedDevice(UniFiNetworkApi api)
        {
            API = api;
        }
    }
}
