using Newtonsoft.Json;
using System;

namespace Viber.Bot
{
    public class PictureMessage : MessageBase
    {
        public PictureMessage() : base(MessageType.Picture) {
            Text = String.Empty;
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}