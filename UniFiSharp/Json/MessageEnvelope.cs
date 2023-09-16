using Newtonsoft.Json;
using System;

namespace UniFiSharp.Json
{
    public class MessageEnvelope
    {
        [JsonProperty(PropertyName = "meta")]
        public MessageEnvelopeMetadata Metadata { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "codeS")]
        public string CodeString { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string ErrorMessage { get; set; }

        public bool IsSuccessfulResponse 
        { 
            get 
            { 
                return this.Message?.Equals("success", StringComparison.OrdinalIgnoreCase) ?? false
                    || (Metadata != null && Metadata.ResultCode.Equals("ok", StringComparison.OrdinalIgnoreCase));
            } 
        }

        public class MessageEnvelopeMetadata
        {
            [JsonProperty(PropertyName = "msg")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "rc")]
            public string ResultCode { get; set; }
        }
    }

    public class MessageEnvelope<T> : MessageEnvelope
    {
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
