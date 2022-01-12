using Newtonsoft.Json;

namespace Viber.Bot
{
    public class UrlMessage : MessageBase
    {
        public UrlMessage() : base(MessageType.Url) { }

        [JsonProperty("media")]
        public string Media { get; set; }
    }
}