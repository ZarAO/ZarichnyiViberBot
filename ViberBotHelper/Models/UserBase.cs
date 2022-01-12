using Newtonsoft.Json;

namespace Viber.Bot
{
    public class UserBase : IUserBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}