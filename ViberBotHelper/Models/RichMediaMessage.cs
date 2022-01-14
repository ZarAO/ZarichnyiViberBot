using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viber.Bot
{
    public class RichMediaMessage : MessageBase
    {
        public RichMediaMessage() : base(MessageType.CarouselContent) { }

        [JsonProperty("buttonsGroupColumns")]
        public int ButtonsGroupColumns { get; set; }

        [JsonProperty("buttonsGroupRows")]
        public int ButtonsGroupRows { get; set; }

        [JsonProperty("bgColor")]
        public string BGColor { get; set; }

        [JsonProperty("buttons")]
        public ICollection<KeyboardButton> Buttons { get; set; }
    }
}
