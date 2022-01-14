using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Viber.Bot
{
    public class CallbackData
    {
        [JsonProperty("event")]
        public EventType Event { get; set; }

        [JsonProperty("message_token")]
        public long MessageToken { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }

        [JsonIgnore]
        public MessageBase Message { get; set; }

        [JsonProperty("message")]
        private JObject message {
            set
            {
                var messageType = value.Property("type").Value.ToObject<MessageType>();
                Type type;
                switch (messageType) {
                    case MessageType.Text:
                        type = typeof(TextMessage);
                        break;
                    case MessageType.Picture:
                        type = typeof(PictureMessage);
                        break;
                    case MessageType.Video:
                        type = typeof(VideoMessage);
                        break;
                    case MessageType.File:
                        type = typeof(FileMessage);
                        break;
                    case MessageType.Location:
                        type = typeof(LocationMessage);
                        break;
                    case MessageType.Contact:
                        type = typeof(ContactMessage);
                        break;
                    case MessageType.Sticker:
                        type = typeof(RichMediaMessage);
                        break;
                    case MessageType.CarouselContent:
                        throw new NotImplementedException();
                    case MessageType.Url:
                        type = typeof(UrlMessage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Message = (MessageBase) value.ToObject(type);
            }
        }
    }
}