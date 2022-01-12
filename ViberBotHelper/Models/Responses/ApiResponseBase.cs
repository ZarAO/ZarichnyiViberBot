using Newtonsoft.Json;

namespace Viber.Bot
{
    internal abstract class ApiResponseBase
    {
        [JsonProperty("status")]
        public ErrorCode Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }
    }
}