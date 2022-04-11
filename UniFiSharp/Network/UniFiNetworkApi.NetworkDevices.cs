﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Json;

namespace UniFiSharp
{
    public partial class UniFiNetworkApi
    {
        protected IUniFiRestClient RestClient;
        public string Site;

        public UniFiNetworkApi(IUniFiRestClient rc, string site)
        {
            this.RestClient = rc;
            this.Site = site;
        }

        /// <summary>
        /// Apply configuration options to a UniFi device managed by this controller
        /// </summary>
        /// <param name="deviceId">Device ID of the UniFi device to configure</param>
        /// <param name="configuration">Configuration element to set</param>
        /// <returns></returns>
        public async Task NetworkDeviceConfigure(string deviceId, NetworkDeviceConfigurations configuration)
        {
            await RestClient.UniFiPut($"api/s/{Site}/rest/device/{deviceId}", configuration.Value);
        }

        /// <summary>
        /// Retrieve a collection of all UniFi devices managed by this controller
        /// </summary>
        /// <returns>Collection of JSON objects describing UniFi devices managed by this controller</returns>
        public async Task<IEnumerable<NetworkDevice>> DeviceList()
        {
            return await RestClient.UniFiGetMany<NetworkDevice>($"api/s/{Site}/stat/device");
        }

        /// <summary>
        /// Retrieve a single UniFi device managed by this controller
        /// </summary>
        /// <param name="macAddress">MAC Address of the UniFi device to retrieve</param>
        /// <returns>JSON object describing the requested UniFi device, or <c>NULL</c></returns>
        public async Task<NetworkDevice> DeviceGet(string macAddress)
        {
            return await RestClient.UniFiGet<NetworkDevice>($"api/s/{Site}/stat/device/{macAddress}");
        }

        /// <summary>
        /// Power-cycle a single port on a UniFi device managed by this controller
        /// </summary>
        /// <param name="macAddress">MAC Address of the UniFi device to power cycle a port on</param>
        /// <param name="portIndex">Index of the port to power cycle</param>
        /// <returns></returns>
        public async Task DevicePowerCyclePort(string macAddress, int portIndex)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr", new
            {
                cmd = "power-cycle",
                mac = macAddress,
                port_idx = portIndex
            });
        }

        /// <summary>
        /// Restart a UniFi device managed by this controller
        /// </summary>
        /// <param name="macAddress">MAC Address of the UniFi device to restart</param>
        /// <returns></returns>
        public async Task DeviceRestart(string macAddress)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr", new
            {
                reboot_type = "soft",
                cmd = "restart",
                mac = macAddress
            });
        }

        /// <summary>
        /// Configure ('adopt') an unconfigured UniFi device to be managed by this controller
        /// </summary>
        /// <param name="macAddress">MAC Address of the UniFi device to adopt</param>
        /// <returns></returns>
        public async Task DeviceAdopt(string macAddress)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr", new
            {
                cmd = "adopt",
                mac = macAddress
            });
        }

        /// <summary>
        /// Remove ('forget') a configured UniFi device from this controller
        /// </summary>
        /// <param name="macAddress">MAC Address of the UniFi device to forget</param>
        /// <returns></returns>
        public async Task DeviceForget(string macAddress)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/sitemgr", new
            {
                cmd = "forget-sta", //"delete-device",
                mac = macAddress
            });
        }

        /// <summary>
        /// Toggle the status of the device LED to flash ("locate mode")
        /// </summary>
        /// <param name="macAddress">Device MAC address</param>
        /// <param name="isFlashingLocator"><c>TRUE</c> if the LED should be flashing, otherwise <c>FALSE</c></param>
        /// <returns></returns>
        public async Task DeviceLocate(string macAddress, bool isFlashingLocator)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr", new
            {
                cmd = isFlashingLocator ? "set-locate" : "unset-locate",
                mac = macAddress
            });
        }

        /// <summary>
        /// Start an RF spectrum scan on an access point (takes ~15min, AP unavailable while running)
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns></returns>
        public async Task DeviceRfScan(string macAddress)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr", new
            {
                cmd = "spectrum-scan",
                mac = macAddress
            });
        }

        /// <summary>
        /// Retrieve the status of an RF spectrum scan on an access point
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns>JSON object describing the status of an RF spectrum scan</returns>
        public async Task<SpectrumScan> DeviceRfScanStatus(string macAddress)
        {
            return await RestClient.UniFiGet<SpectrumScan>($"api/s/{Site}/stat/spectrum-scan/{macAddress}");
        }

        /// <summary>
        /// Upgrade the firmware on a device
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <returns></returns>
        public async Task DeviceUpgrade(string macAddress)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/devmgr/upgrade", new { mac = macAddress });
        }

        /// <summary>
        /// Set the volume of the device's speaker
        /// </summary>
        /// <param name="macAddress">Device MAC Address</param>
        /// <param name="volume">Volume to set the speaker to (range 0 - 100)</param>
        /// <returns></returns>
        public async Task DeviceSetVolume(string macAddress, int volume)
        {
            await RestClient.UniFiPost($"api/s/{Site}/cmd/streammgr/set-volume", new
            {
                cmd = "set-volume",
                mac = macAddress,
                volume = volume
            });
        }
    }
}
