using Newtonsoft.Json;
using System.Collections.Generic;

namespace Viber.Bot
{
    public class Keyboard
    {
        [JsonProperty("Type")]
        private const string Type = "keyboard";

        [JsonProperty("Buttons")]
        public ICollection<KeyboardButton> Buttons { get; set; }

        [JsonProperty("DefaultHeight")]
        public bool DefaultHeight { get; set; }

        [JsonProperty("BgColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("CustomDefaultHeight")]
        public int? CustomDefaultHeight { get; set; }

        [JsonProperty("HeightScale")]
        public int? HeightScale { get; set; }

        [JsonProperty("ButtonsGroupColumns")]
        public int? ButtonsGroupColumns { get; set; }

        [JsonProperty("ButtonsGroupRows")]
        public int? ButtonsGroupRows { get; set; }

        [JsonProperty("InputFieldState")]
        public KeyboardInputFieldState? InputFieldState { get; set; }
    }
}
