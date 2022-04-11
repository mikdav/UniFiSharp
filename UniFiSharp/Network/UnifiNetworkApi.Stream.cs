using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Json;

namespace UniFiSharp
{
    public partial class UniFiNetworkApi
    {
        // This partial class is for managing media streams, typically to UniFi EDU APs

        /// <summary>
        /// Plays a sample file on a specific access point.
        /// </summary>
        /// <returns>Collection of JSON objects describing sample media files stored on this controller</returns>
        public async Task<IEnumerable<StreamStatus>> SampleStreamStart(string filename, string macAddress)
        {
            return await RestClient.UniFiPostMany<StreamStatus>($"api/s/{Site}/cmd/streammgr/sample-stream", new
            {
                cmd = "sample-stream",
                mac = macAddress,
                sample_filename = filename
            });
        }


        /// <summary>
        /// Creates a stream with the specified options. 
        /// </summary>
        /// <param name="streamInfo">Options for the specified stream, such as broadcast group and file</param>
        /// <returns>Details of the created stream</returns>
        public async Task<StreamStatus> StreamCreate(StreamInfo streamInfo)
        {
            return await RestClient.UniFiPost<StreamStatus>($"api/s/{Site}/cmd/streammgr/create-stream", streamInfo);
        }

        /// <summary>
        /// Stops a stream with a given ID
        /// </summary>
        /// <param name="streamId">ID of the stream to start</param>
        /// <returns>Status of the stopped stream</returns>
        public async Task<StreamStatus> StreamStop(string streamId)
        {
            return await RestClient.UniFiPost<StreamStatus>($"api/s/{Site}/cmd/streammgr/stop-stream", new
            {
                cmd = "stop-stream",
                streamId = streamId
            });
        }

        /// <summary>
        /// Starts a stream with a given ID
        /// </summary>
        /// <param name="streamId">ID of the stream to start</param>
        /// <returns>Status of the started stream</returns>
        public async Task<StreamStatus> StreamStart(string streamId)
        {
            return await RestClient.UniFiPost<StreamStatus>($"api/s/{Site}/cmd/streammgr/start-stream", new
            {
                cmd = "start-stream",
                streamId = streamId
            });
        }
        
        public async Task<IEnumerable<StreamStatus>> ActiveStreamList()
        {
            return await RestClient.UniFiGetMany<StreamStatus>($"api/s/{Site}/stat/stream");
        }
    }
}
