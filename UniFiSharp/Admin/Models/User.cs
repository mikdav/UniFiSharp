using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniFiSharp.Admin.Models
{
    [JsonObject]
    public class User
    {
        // Gets the unique Id of the user
        public string Unique_Id { get; set; }
        
        // Gets the first name of the user.
        public string First_Name { get; set; }

        // Gets the last name of the user.
        public string Last_Name { get; set; }

        // Gets the email of the user.
        public string User_Email { get; set; }

        // Determines if NFC should be forcefully added.
        public bool Force_Add_Nfc { get; set; }

        // Gets the NFC token of the user.
        public string Nfc_Token { get; set; }

        // Gets the pin code of the user.
        public string Pin_Code { get; set; }

        // Gets the activation status of the user account.
        public string Status { get; set; }

        // Gets the employee number of the user.
        public string Employee_Number { get; set; }

        // Gets the creation time of the user account.
        public int Create_Time { get; set; }

        // Gets the group IDs associated with the user.
        public List<string> Group_Ids { get; set; }
    }
}
