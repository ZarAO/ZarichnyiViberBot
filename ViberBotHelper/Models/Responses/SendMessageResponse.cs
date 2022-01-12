using Newtonsoft.Json;

namespace Viber.Bot
{
    internal class SendMessageResponse : ApiResponseBase
    {
        [JsonProperty("message_token")]
        public long MessageToken { get; set; }
    }
}