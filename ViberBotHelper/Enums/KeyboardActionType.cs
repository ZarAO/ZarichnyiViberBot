using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Viber.Bot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KeyboardActionType
    {
        [EnumMember(Value = "reply")]
        Reply = 1,
        [EnumMember(Value = "open-url")]
        OpenUrl = 2,
        [EnumMember(Value = "location-picker")]
        LocationPicker = 3,
        [EnumMember(Value = "share-phone")]
        SharePhone = 4,
        [EnumMember(Value = "none")]
        None = 5
    }
}