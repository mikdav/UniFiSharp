using UniFiSharp.Json;

namespace UniFiSharp.Orchestration.Models
{
    public class UserGroupModel
    {
        public string UserGroupId => Json._id;
        public int QoSRateMaxDown => Json.qos_rate_max_down;
        public int QoSRateMaxUp => Json.qos_rate_max_up;
        public string Name => Json.name;
        
        private UserGroup Json { get; set; }
        public UserGroupModel(UserGroup json)
        {
            Json = json;
        }

        public static UserGroupModel CreateFromJson(UserGroup json)
        {
            return new UserGroupModel(json);
        }
    }
}
