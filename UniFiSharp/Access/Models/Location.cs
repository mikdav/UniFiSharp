using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    public abstract class Location
    {
        public string Unique_Id { get; set; }

        public string Name { get; set; }

        public string Up_Id { get; set; }

        public string TimeZone { get; set; }

        public virtual string Location_Type { get; set; }

        public string Extra_Type { get; set; }

        public string Full_Name { get; set; }

        public int Level { get; set; }

        public string Work_Time { get; set; }

        public string Work_Time_Id { get; set; }

        public Dictionary<string, string> Extras { get; set; }
    }
}
