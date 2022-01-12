using Newtonsoft.Json;

namespace Viber.Bot
{
    internal class GetUserDetailsResponse : ApiResponseBase
    {
        [JsonProperty("message_token")]
        public long MessageToken { get; set; }

        [JsonProperty("user")]
        public UserDetails User { get; set; }
    }
}