using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Viber.Bot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChatMemberRole
    {
        Admin = 1,
        Participant = 2
    }
}