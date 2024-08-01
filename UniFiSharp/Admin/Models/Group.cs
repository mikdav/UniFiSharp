using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniFiSharp.Admin.Models
{
    [JsonObject]
    public class Group
    {
        // Gets or sets the unique Id of the group.
        public string Unique_Id { get; set; }

        // Gets or sets the name Id of the group.
        public string Name { get; set; }

        // Gets or sets the unique Id of the group.
        public string Up_Id { get; set; }

        // Gets the unique Id of the group.
        public List<string> Up_Ids { get; set; }

        // Gets the unique Id of the group.
        public string System_Name { get; set; }

        // Gets the unique Id of the group.
        public DateTime Create_Time { get; set; }
    }
}
