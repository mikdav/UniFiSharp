using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UniFiSharp.Access.Models;
using UniFiSharp.Json;

namespace UniFiSharp.Access
{
    public partial class UniFiAccessApi
    {
        private const int CODE_CREDS_NFC_READ_POLL_TOKEN_EMPTY = 660002;

        /// <summary>
        /// Puts a reader into reading mode and waits for a card to be read.
        /// Once read, the reader returns the encryption token of the card that was read.
        /// </summary>
        /// <param name="macAddress">The mac address of the reader to use when enrolling the card.</param>
        /// <param name="timeoutSeconds">The number of seconds to wait for a user to tap a card to the reader.</param>
        /// <returns>The encrypt token of the card.</returns>
        /// <exception cref="TimeoutException">Throws TimeoutException if a badge isn't tapped to the reader in the supplied timeout</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException if supplied timeout is less than 1 seconds</exception>
        public async Task<string> EnrollAccessCard(string macAddress, int timeoutSeconds = 60)
        {
            if (timeoutSeconds < 1)
            {
                throw new ArgumentException("Timeout cannot be less than a second.", nameof(timeoutSeconds));
            }

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
            string sessionUrl = $"access/api/v2/nfcs/session/{sessionId}";

            Stopwatch stopwatch = Stopwatch.StartNew();

            do
            {
                if (stopwatch.Elapsed.TotalSeconds > timeoutSeconds)
                {
                    // Best effort terminate nfcs session
                    await this.RestClient.UniFiDelete(sessionUrl);
                    throw new TimeoutException($"A card was not read by the reader within the timeout of {timeoutSeconds} seconds.");
                }

                await Task.Delay(1000);
                pollResult = (MessageEnvelope<object>)await this.RestClient.UniFiGet(sessionUrl);
            }
            // Earlier versions of the Unifi Access API didn't return a code, so we had to check the message
            // Leaving the message check here for compatibility across firmware versions
            while (pollResult.Code == CODE_CREDS_NFC_READ_POLL_TOKEN_EMPTY || pollResult.Message.Equals("NFC token is empty", StringComparison.OrdinalIgnoreCase));

            return (pollResult.Data as JObject)["encrypt_token"].ToString();
        }
    }
}
