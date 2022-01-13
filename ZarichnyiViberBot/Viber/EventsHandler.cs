using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viber.Bot;

namespace ViberBotServer.Viber
{
    public class EventsHandler
    {
        private readonly ViberBot _viberBot;

        public EventsHandler(ViberBot viberBot) {
            _viberBot = viberBot;
        }

        public async Task onReceiveMessage(string senderId, long messageToken, MessageBase message) {
            switch (message.Type) {
                case MessageType.Text: {
                    TextMessage textMessage = (TextMessage) message;
                    await _viberBot.SendTextMessageAsync(textMessage.Text, senderId);
                    break;
                }
            }
        }

        public async Task onSubscribed(string userId) {
            await _viberBot.SendTextMessageAsync("Subscribed", userId);
        }

        public async Task onUnSubscribed(string userId) {
            await _viberBot.SendTextMessageAsync("UnSubscribed", userId);
        }

        public async Task onConversationStarted(string userId, string userName, string userAvatarUrl) {
            string welcomeMessage = Strings.STR_WELCOME;



            await _viberBot.SendTextMessageAsync(welcomeMessage, userId);
        }
    }
}
