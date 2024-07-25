using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UniFiSharp.Admin.Models;

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
        /// Returns a collection of all the users created in a UDM.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await RestClient.UniFiGetMany<User>($"access/ulp-go/api/v2/users/search");
        }

        /// <summary>
        /// Returns the specified user data.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns></returns>
        public async Task<User> GetUser(string email)
        {
            var users = await RestClient.UniFiGetMany<User>($"/access/ulp-go/api/v2/users/search?condition={email}");
            return users.Single();
        }

        /// <summary>
        /// Deactivates the user account.
        /// </summary>
        /// <param name="email">The email of the user acccount that needs to be deactivated.</param>
        /// <returns></returns>
        public async Task<User> DeactivateUser(string email)
        {
            try
            {
                var user = await GetUser($"{email}");

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var uniqueId = user.Unique_Id;

                user.Status = "DEACTIVATED";

                var jsonBody = JsonConvert.SerializeObject(user);

                await RestClient.UniFiPut($"/users/api/v2/user/{uniqueId}/deactivate?isULP=1", jsonBody);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Activates the user account.
        /// </summary>
        /// <param name="email">The email of the user acccount that needs to be activated.</param>
        /// <returns></returns>
        public async Task<User> ActivateUser(string email)
        {
            try
            {
                var user = await GetUser($"{email}");

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                
                var uniqueId = user.Unique_Id;

                user.Status = "ACTIVE";

                var jsonBody = JsonConvert.SerializeObject(user);

                await RestClient.UniFiPut($"/users/api/v2/user/{uniqueId}/active?isULP=1", jsonBody);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Return all the access groups in the UDM.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Group>> GetAllGrpoups()
        {
            return await RestClient.UniFiGetMany<Group>($"/users/api/v2/user_groups");
        }

        /// <summary>
        /// Returns the specified group or groups
        /// </summary>
        /// <param name="names">Names of the groups.</param>
        /// <param name="uniqueIds">Unique IDs of the groups.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Group>> GetGroup(List<string> names = null, List<string> uniqueIds = null)
        {
            if (names == null && uniqueIds == null)
            {
                throw new ArgumentException("Both name and uniqueId cannot be null");
            }

            try
            {
                var groups = await GetAllGrpoups();
                
                List<Group> foundGroups = new List<Group>();

                if ()
                { 
                
                }
                
                foreach (var name in names)
                {
                    if (name != null)
                    {
                        foundGroups.Add(groups.SingleOrDefault(g => g.Name == name));
                    }
                }

                foreach (var uniqueId in uniqueIds)
                {
                    if (uniqueId != null)
                    {
                        foundGroups.Add(groups.SingleOrDefault(g => g.Unique_Id == uniqueId));
                    }
                }

                if (foundGroups.Count() == 0)
                {
                    throw new Exception("Group not found");
                }

                return foundGroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<User> AddToGroup(string email, List<Group> groups)
        { 
            User user = await GetUser(email);

            var userUniqueId = user.Unique_Id;

            foreach (var group in groups)
            {
                user.Groups.Add(group);
            }

            //var jsonBody = JsonConvert.SerializeObject(user);

            return await RestClient.UniFiPut<User>($"/users/api/v2/user/{userUniqueId}", user);
        }

    }
}
