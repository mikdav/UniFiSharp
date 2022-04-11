namespace UniFiSharp.Json
{
    public class StreamStatus
    {
        public string streamId { get; set; }
        public bool ready { get; set; }
        public bool streaming { get; set; }
        public NetworkDevice[] devices { get; set; }
        public string type { get; set; }
        public string sample_filename { get; set; }
        public string mediafile_id { get; set; }
    }
}