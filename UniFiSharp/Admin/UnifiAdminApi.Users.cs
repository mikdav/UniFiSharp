using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UniFiSharp.Admin.Models;
using UniFiSharp.Json;

namespace UniFiSharp.Admin
{
    public partial class UniFiAdminApi
    {
        protected IUniFiRestClient RestClient;

        public UniFiAdminApi(IUniFiRestClient rc)
        {
            this.RestClient = rc;
        }

        /// <summary>
        /// Creates a new local user in the UDM.
        /// </summary>
        /// <param name="fisrtName">User's name.</param>
        /// <param name="lastName">User's lastname.</param>
        /// <param name="email">Company email of the new user.</param>
        /// <param name="employeeNumber">Employee Id of the new user.</param>
        /// <param name="groups">Name or names of the groups the user needs to be part of.</param>
        /// <returns></returns>
        public async Task<UniFiSharp.Admin.Models.User> CreateUser(string fisrtName, string lastName, string email, string employeeNumber, List<string> groups)
        {
            var isExistingUser = await GetUser(email);

            if(isExistingUser != null && isExistingUser.Status != "ACTIVE")
            {
                await ActivateUser(email);

                return null;
            }
            
            UniFiSharp.Admin.Models.User newUser = new UniFiSharp.Admin.Models.User()
            {
                First_Name = fisrtName,
                Last_Name = lastName,
                User_Email = email,
                Employee_Number = employeeNumber,
                Force_Add_Nfc = true,
                Nfc_Token = string.Empty,
                Pin_Code = string.Empty,
                Group_Ids = new List<string>()
            };

            var udmGroups = await GetAllGrpoups();

            foreach (var group in groups)
            {
                var groupToAdd = udmGroups.SingleOrDefault(u => u.Name == group);

                if (groupToAdd == null)
                {
                    throw new Exception($"The group {group} is not part of the UDM group list");
                }

                newUser.Group_Ids.Add(groupToAdd.Unique_Id);
            }

            await RestClient.UniFiPost($"/users/api/v2/user", newUser);

            return null;
        }

        /// <summary>
        /// Returns all the existing groups in a UDM.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Group>> GetAllGrpoups()
        {
            var groups = await RestClient.UniFiGetMany<Group>($"/users/api/v2/user_groups");

            if (!groups.Any())
            {
                throw new Exception("No groups were found");
            }
            
            return groups;
        }

        /// <summary>
        /// Returns a collection of all the users created in a UDM.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UniFiSharp.Admin.Models.User>> GetAllUsers()
        {
            return await RestClient.UniFiGetMany<UniFiSharp.Admin.Models.User>($"access/ulp-go/api/v2/users/search");
        }

        /// <summary>
        /// Returns the specified user data.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns></returns>
        public async Task<UniFiSharp.Admin.Models.User> GetUser(string email)
        {
            var users = await RestClient.UniFiGetMany<UniFiSharp.Admin.Models.User>($"access/ulp-go/api/v2/users/search?condition={email}");

            if(users.Count == 0)
            {
                return null;
            }

            return users.Single();
        }

        /// <summary>
        /// Deactivates the user account.
        /// </summary>
        /// <param name="email">The email of the user acccount that needs to be deactivated.</param>
        /// <returns></returns>
        public async Task<UniFiSharp.Admin.Models.User> DeactivateUser(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Status == "DEACTIVATED")
            {
                return null;
            }

            user.Status = "DEACTIVATED";

            var jsonBody = JsonConvert.SerializeObject(user);

            await RestClient.UniFiPut($"/users/api/v2/user/{user.Unique_Id}/deactivate?isULP=1", jsonBody);
            
            return null;
        }

        /// <summary>
        /// Activates the user account.
        /// </summary>
        /// <param name="email">The email of the user acccount that needs to be activated.</param>
        /// <returns></returns>
        public async Task<UniFiSharp.Admin.Models.User> ActivateUser(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Status == "ACTIVE")
            {
                return null;
            }

            user.Status = "ACTIVE";

            var jsonBody = JsonConvert.SerializeObject(user);

            await RestClient.UniFiPut($"/users/api/v2/user/{user.Unique_Id}/active?isULP=1", jsonBody);
            
            return null;
        }

        /// <summary>
        /// Deletes the speciafied user. User account has to be deactivated
        /// </summary>
        /// <param name="email">The email of the user account to bedeleted.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UniFiSharp.Admin.Models.User> DeleteUser(string email)
        {
            var user = await GetUser(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!(user.Status == "DEACTIVATED"))
            {
                await DeactivateUser(email);
            }

            await RestClient.UniFiDelete($"/users/api/v2/user/{user.Unique_Id}?isULP=1");

            return null;
        }
    }
}
