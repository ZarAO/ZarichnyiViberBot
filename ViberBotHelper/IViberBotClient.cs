using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viber.Bot
{
    public interface IViberBotClient
    {
        Task<ICollection<EventType>> SetWebhookAsync(string url, ICollection<EventType> eventTypes = null);
        Task<IAccountInfo> GetAccountInfoAsync();
        Task<UserDetails> GetUserDetailsAsync(string id);
        Task<long> SendTextMessageAsync(TextMessage message);
        Task<long> SendPictureMessageAsync(PictureMessage message);
        Task<long> SendFileMessageAsync(FileMessage message);
        Task<long> SendVideoMessageAsync(VideoMessage message);
        Task<long> SendContactMessageAsync(ContactMessage message);
        Task<long> SendLocationMessageAsync(LocationMessage message);
        Task<long> SendUrlMessageAsync(UrlMessage message);
        Task<long> SendStickerMessageAsync(StickerMessage message);
        Task<long> SendKeyboardMessageAsync(KeyboardMessage message);
        Task<long> SendRichMediaMessageAsync(RichMediaMessage message);
        Task<long> SendBroadcastMessageAsync(BroadcastMessage message);
        bool ValidateWebhookHash(string signatureHeader, string jsonMessage);
    }
}