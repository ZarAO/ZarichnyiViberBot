using Newtonsoft.Json;

namespace Viber.Bot
{
    public class StickerMessage : MessageBase
    {
        public StickerMessage() : base(MessageType.Sticker) { }

        [JsonProperty("sticker_id")]
        public string StickerId { get; set; }
    }
}