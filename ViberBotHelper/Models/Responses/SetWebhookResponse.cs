using Newtonsoft.Json;
using System.Collections.Generic;

namespace Viber.Bot
{
    internal class SetWebhookResponse : ApiResponseBase
    {
        [JsonProperty("event_types")]
        public ICollection<EventType> EventTypes { get; set; }
    }
}