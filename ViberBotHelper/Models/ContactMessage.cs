using Newtonsoft.Json;

namespace Viber.Bot
{
	public class ContactMessage : MessageBase
	{
		public ContactMessage() : base(MessageType.Contact) {
		}

		[JsonProperty("contact")]
		public Contact Contact { get; set; }
	}
}
