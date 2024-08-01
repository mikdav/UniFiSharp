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
                await api.Authenticate();

                var testDevice = await api.Access.GetDevice("e4:38:83:ba:eb:1f");
                
            }
        }
    }
}