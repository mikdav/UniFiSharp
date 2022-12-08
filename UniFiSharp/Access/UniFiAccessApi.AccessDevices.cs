using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Access.Models;

namespace UniFiSharp.Access
{
    public partial class UniFiAccessApi
    {
        protected IUniFiRestClient RestClient;
        protected string Site;

        public UniFiAccessApi(IUniFiRestClient rc) {
            this.RestClient = rc;
        }
        
        /// <summary>
        /// Retrieve a collection of all UniFi devices managed by this controller
        /// </summary>
        /// <returns>Collection of JSON objects describing UniFi devices managed by this controller</returns>
        public async Task<IEnumerable<AccessDevice>> DeviceList()
        {
            return await RestClient.UniFiGetMany<AccessDevice>($"access/api/v2/devices");
        }

        /// <summary>
        ///  Retrieve a single AccessDevice by MAC Address
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns>The task result contains a single retrieved AccessDevice</returns>
        public async Task<AccessDevice> GetDevice(string macAddress)
        {
            return await RestClient.UniFiGet<AccessDevice>($"access/api/v2/device/{macAddress.ToLowerInvariant().Replace(":", string.Empty)}");
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await RestClient.UniFiGetMany<Building>("access/ulp-go/api/v2/locations/buildings?with_children=true");
        }

        /// <summary>
        /// Unlock a device
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns>The task result contains a value indicating if the Unlock was successful</returns>
        public async Task<bool> UnlockDevice(string macAddress)
        {
            string result = await RestClient.UniFiPut<string>($"access/api/v2/device/{macAddress.ToLowerInvariant().Replace(":", string.Empty)}/relay_unlock", null);
            if (!result.Equals("success"))
            {
                return false;
            }

            return true;
        }
    }
}
