using Newtonsoft.Json;

namespace Viber.Bot
{
    public class FileMessage : MessageBase
    {
        public FileMessage() : base(MessageType.File) {
        }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }
    }
}