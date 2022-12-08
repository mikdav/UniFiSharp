using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace UniFiSharp.Access.Models
{
    public enum VisitReason
    {
        Guest,
        Business,
        Contractor,
        Delivery
    }

    [JsonObject]
    public class Visitor
    {
        private List<AccessCard> accessCards = new List<AccessCard>();
        private List<ResourceLink> resources = new List<ResourceLink>();
        private List<Guid> visitingUserIds = new List<Guid>();

        [JsonProperty("unique_id")]
        public Guid? Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("mobile_phone")]
        public string MobilePhone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("start_time")]
        public uint StartEpochTimestamp { get; set; }

        [JsonProperty("end_time")]
        public uint EndEpochTimestamp { get; set; }

        [JsonProperty("visit_reason")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VisitReason VisitReason { get; set; }

        [JsonProperty("visitor_company")]
        public string Company { get; set; }

        [JsonIgnore]
        public DateTime StartTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(this.StartEpochTimestamp).DateTime;
            }

            set
            {
                this.StartEpochTimestamp = (uint)new DateTimeOffset(value).ToUnixTimeSeconds();
            }
        }

        [JsonIgnore]
        public DateTime EndTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(this.EndEpochTimestamp).DateTime;
            }

            set
            {
                this.EndEpochTimestamp = (uint)new DateTimeOffset(value).ToUnixTimeSeconds();
            }
        }

        [JsonProperty("nfc_cards")]
        public IList<AccessCard> AccessCards
        {
            get
            {
                return this.accessCards;
            }
        }

        [JsonProperty("resource")]
        public IList<ResourceLink> Resources
        {
            get
            {
                return this.resources;
            }
        }

        [JsonProperty("inform_user_ids")]
        public IList<Guid> VisitingUserIds
        {
            get
            {
                return this.visitingUserIds;
            }
        }
    }
}
