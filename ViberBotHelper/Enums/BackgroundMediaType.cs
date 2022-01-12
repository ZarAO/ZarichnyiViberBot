using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Viber.Bot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BackgroundMediaType
    {
        [EnumMember(Value = "picture")]
        Picture = 1,

        [EnumMember(Value = "gif")]
        Gif = 2
    }
}