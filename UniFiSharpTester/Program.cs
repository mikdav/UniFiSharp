using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks.Dataflow;
using UniFiSharp;
using UniFiSharp.Admin.Models;


namespace UniFiSharpTester
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var api = new UniFiApi(new Uri("https://ATL-UDM.corp.nirvc.com"), "unifiauto", "8XP5%9LKbw1@A3%ZF%jP8I0*ZH$y^W", useModernApi: true))
            {
                var loginResult = await api.Authenticate();

                // Target user
                string email = "testerson@nirvc.com";
                
                // Test group names
                string mgmtGroup = "Management";
                string ITGroup = "IT";

                List<string> groups = new List<string>();
                groups.Add(mgmtGroup);
                groups.Add(ITGroup);

                // Gets all the users.
                //var users = await api.Admin.GetAllUsers();
                //var totalUsers = users.Count();

                // Gets the sopecified user by email.
                //var user = await api.Admin.GetUser(email);

                // Deactivates the specified user
                //await api.Admin.DeactivateUser(email);

                // Activates the specified user
                //await api.Admin.ActivateUser(email);

                // Gets the specified group.
                var group =  await api.Admin.GetGroup(groups, null);

                //Adds the specified user to a single or multiple groups.
                await api.Admin.AddToGroup(email, group);


            }
        }
    }
}