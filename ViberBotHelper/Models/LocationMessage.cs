using Newtonsoft.Json;

namespace Viber.Bot
{
    public class LocationMessage : MessageBase
    {
        public LocationMessage() : base(MessageType.Location) {
        }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}