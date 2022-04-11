using UniFiSharp.Json;

namespace UniFiSharp.Orchestration.Models
{
    public class WlanGroupModel
    {
        public string WlanGroupId => Json._id;
        public string RoamRadio => Json.roam_radio;
        public string Name => Json.name;
        public string PmfMode => Json.pmf_mode;
        public int RoamChannelNA => Json.roam_channel_na.GetValueOrDefault(0);
        public int RoamChannelNG => Json.roam_channel_ng.GetValueOrDefault(0);
        
        private WlanGroup Json { get; set; }
        public WlanGroupModel(WlanGroup json)
        {
            Json = json;
        }

        public static WlanGroupModel CreateFromJson(WlanGroup json)
        {
            return new WlanGroupModel(json);
        }
    }
}
