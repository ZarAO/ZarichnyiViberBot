using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Viber.Bot
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OpenUrlType
	{
		[EnumMember(Value = "internal")]
		Internal = 1,
		[EnumMember(Value = "external")]
		External = 2
	}
}