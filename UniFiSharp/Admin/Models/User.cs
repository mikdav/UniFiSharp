using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniFiSharp.Admin.Models
{
    [JsonObject]
    public class User
    {
        public int Id { get; set; }
        
        public string Unique_Id { get; set; }
        
        public string First_Name { get; set; }
        
        public string Last_Name { get; set; }
        
        public string Alias { get; set; }
        
        public string Full_Name { get; set; }
        
        public string Email { get; set; }
        
        public string User_Email { get; set; }

        public string Email_Status { get; set; }
        
        public bool Email_Is_Null { get; set; }

        public string Status { get; set; }

        public string Employee_Number { get; set; }

        public long Create_Time { get; set; }
        
        public string Username { get; set; }
        
        public string Sso_Account { get; set; }

        public string Sso_Uuid { get; set; }
        
        public string Sso_Username { get; set; }

        public List<Group> Groups { get; set; }
        
        public bool Cloud_Access_Granted { get; set; }
        
        public long Update_Time { get; set; }
        
        public long Last_Activity_Time { get; set; }
    }
}
