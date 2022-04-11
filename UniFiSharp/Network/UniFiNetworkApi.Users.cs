using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Json;

namespace UniFiSharp
{
    public partial class UniFiNetworkApi
    {
        public async Task<IEnumerable<User>> UserList()
        {
            return await RestClient.UniFiGetMany<User>($"api/s/{Site}/stat/alluser");
        }

        public async Task<User> UserGet(string macAddress)
        {
            return await RestClient.UniFiGet<User>($"api/s/{Site}/stat/user/{macAddress}");
        }


        public async Task UserSetUsergroup(User user, UserGroup userGroup)
        {
            await RestClient.UniFiPut($"api/s/{Site}/rest/user/{user._id}", new
            {
                usergroup_id = userGroup?._id ?? string.Empty
            });
        }

        public async Task UserUnsetUsergroup(User user)
        {
            await RestClient.UniFiPut($"api/s/{Site}/rest/user/{user._id}", new
            {
                usergroup_id = string.Empty
            });
        }
    }
}
