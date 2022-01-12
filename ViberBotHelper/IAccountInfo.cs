using System.Collections.Generic;

namespace Viber.Bot
{
    public interface IAccountInfo
    {
        string Id { get; }
        string Name { get; }
        string Uri { get; }
        string Icon { get; }
        string Background { get; }
        string Category { get; }
        string Subcategory { get; }
        ICollection<EventType> EventTypes { get; }
        Location Location { get; }
        string Country { get; }
        string Webhook { get; }
        long SubscribersCount { get; }
        ICollection<ChatMember> Members { get; }
    }
}