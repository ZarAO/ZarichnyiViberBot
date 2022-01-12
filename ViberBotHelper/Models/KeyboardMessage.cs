using Newtonsoft.Json;

namespace Viber.Bot
{
    public class KeyboardMessage : MessageBase
    {
        public KeyboardMessage()
            : base(MessageType.Text) {
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("keyboard")]
        public Keyboard Keyboard { get; set; }
    }
}