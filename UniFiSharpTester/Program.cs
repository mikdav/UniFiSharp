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

                string firstName = "User";
                string lastName = "ToDelete";
                string email = "usertodelete@nirvc.com";
                string employeeNumber = "1223334444";
                List<string> groups = new List<string>();
                groups.Add("IT");
                groups.Add("Not-Parts");

                ////////////////////////////////////////////////////////////////////////////////////////

                ////Gets all the users.
                //var users = await api.Admin.GetAllUsers();
                //var totalUsers = users.Count();

                //Creates a new user
                await api.Admin.CreateUser(firstName, lastName, email, employeeNumber,groups);
                
                ////Gets the sopecified user by email.
                //var user = await api.Admin.GetUser(email);
                //
                ////Deactivates the specified user
                //await api.Admin.DeactivateUser(email);
                //
                ////Activates the specified user
                //await api.Admin.ActivateUser(email);
                //
                ////Deletes the specified user.
                //await api.Admin.DeleteUser(email);
            }
        }
    }
}