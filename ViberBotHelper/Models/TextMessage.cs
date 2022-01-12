using Newtonsoft.Json;

namespace Viber.Bot
{
    public class TextMessage : MessageBase
    {
        public TextMessage() : base(MessageType.Text) { }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}