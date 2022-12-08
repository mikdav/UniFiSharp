using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using UniFiSharp.Access.Models;
using UniFiSharp.Json;

namespace UniFiSharp.Access
{
    public partial class UniFiAccessApi
    {
        /// <summary>
        /// Puts a scanner into reading mode and waits for a card to be read.
        /// Once read, the scanner returns the encryption token of the card that was read.
        /// </summary>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        public async Task<string> EnrollAccessCard(string macAddress)
        {
            var result = await this.RestClient.UniFiPost<JObject>(
                "access/api/v2/nfcs/session",
                new NfcsSessionRequest()
                {
                    DeviceId = macAddress.ToLowerInvariant().Replace(":", string.Empty),
                    HasLock = false,
                    UserId = string.Empty
                });

            string sessionId = (string)result["session_id"];
            MessageEnvelope<object> pollResult;

            do
            {
                await Task.Delay(1000);
                pollResult = (MessageEnvelope<object>)await this.RestClient.UniFiGet($"access/api/v2/nfcs/session/{sessionId}");
            }
            while (pollResult.Message.Equals("NFC token is empty", StringComparison.OrdinalIgnoreCase));

            return (pollResult.Data as JObject)["encrypt_token"].ToString();
        }
    }
}
