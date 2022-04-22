using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniFiSharp.Access.Models
{
    [JsonObject]
    public class AccessDevice
    {
        public string Unique_Id { get; set; }

        public string Name { get; set; }

        public string Device_Type { get; set; }

        public string Connected_Uah_Id { get; set; }

        public string Location_Id { get; set; }

        public string Firmware { get; set; }

        public string Version { get; set; }

        public string Ip { get; set; }

        public string Mac { get; set; }

        public long Start_Time { get; set; }

        public bool Security_Check { get; set; }

        public string Resource_Name { get; set; }

        public string Revision { get; set; }

        public long Revision_Update_Time { get; set; }

        public long Version_Update_Time { get; set; }

        public long Firmware_Update_Time { get; set; }

        public long Adopt_Time { get; set; }

        public Location Location { get; set; }

        public IEnumerable<DeviceConfig> Configs { get; set; }

        public bool Is_Connected { get; set; }

        public bool Is_Online { get; set; }

        public bool Is_Revision_Up_To_Date { get; set; }

        public bool Is_Adopted { get; set; }

        public bool Is_Managed { get; set; }

    }
}
