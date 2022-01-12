using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Viber.Bot
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TextSize
	{
		[EnumMember(Value = "small")]
		Small = 1,
		[EnumMember(Value = "regular")]
		Regular = 2,
		[EnumMember(Value = "large")]
		Large = 3
	}
}