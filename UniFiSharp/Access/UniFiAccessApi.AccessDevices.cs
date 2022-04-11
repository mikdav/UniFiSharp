using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Json;

namespace UniFiSharp
{
    public class UniFiAccessApi
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
        /// Unlock a device
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns></returns>
        public async Task UnlockDevice(string macAddress)
        {
            string result = await RestClient.UniFiPut<string>($"access/api/v2/device/{macAddress.ToLowerInvariant().Replace(":", string.Empty)}/relay_unlock", null);
            if (!result.Equals("success"))
            {
                throw new InvalidOperationException($"Failed to unlock {macAddress}");
            }
        }
    }
}
