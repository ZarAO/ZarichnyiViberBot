using Newtonsoft.Json;
using System.Collections.Generic;

namespace Viber.Bot
{
    public class BroadcastMessage : MessageBase
    {
        public BroadcastMessage() : base(MessageType.Text) {
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("broadcast_list")]
        public IList<string> BroadcastList { get; set; }
    }
}