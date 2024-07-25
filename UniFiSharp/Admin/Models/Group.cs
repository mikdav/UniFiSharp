using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniFiSharp.Admin.Models
{
    [JsonObject]
    public class Group
    {
        public string Unique_Id { get; set; }

        public string Name { get; set; }

        public string Up_Id { get; set; }

        public List<string> Up_Ids { get; set; }

        public string System_Name { get; set; }

        public DateTime Create_Time { get; set; }
    }
}
