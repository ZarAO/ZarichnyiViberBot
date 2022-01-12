using Newtonsoft.Json;

namespace Viber.Bot
{
    public class VideoMessage : MessageBase
    {
        public VideoMessage() : base(MessageType.Video) { }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }
    }
}