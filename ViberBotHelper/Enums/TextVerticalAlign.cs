using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Viber.Bot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TextVerticalAlign
    {
        [EnumMember(Value = "top")]
        Top = 1,
        [EnumMember(Value = "middle")]
        Middle = 2,
        [EnumMember(Value = "bottom")]
        Bottom = 3
    }
}