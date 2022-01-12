using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Viber.Bot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KeyboardInputFieldState
    {
        [EnumMember(Value = "regular")]
        Regular = 1,
        [EnumMember(Value = "minimized")]
        Minimized = 2,
        [EnumMember(Value = "hidden")]
        Hidden = 3
    }
}