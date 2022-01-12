using Newtonsoft.Json;

namespace Viber.Bot
{
    public class ChatMember : UserBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("role")]
        public ChatMemberRole Role { get; set; }
    }
}