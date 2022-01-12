using Newtonsoft.Json;

namespace Viber.Bot
{
    public class Contact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone_number")]
        public string TN { get; set; }
    }
}