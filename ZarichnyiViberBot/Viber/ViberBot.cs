using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viber.Bot;

namespace ViberBotServer.Viber
{
    public class ViberBot
    {
        public IViberBotClient viberBotClient;
        public ViberBotOptions viberBotOptions;

        public ViberBot(ViberBotOptions viberBotOptions) {
            this.viberBotOptions = viberBotOptions;
            viberBotClient = new ViberBotClient(this.viberBotOptions.AuthenticationToken);
        }

        public async Task GetAccountInfoAsync() {
            var result = await viberBotClient.GetAccountInfoAsync();
            return;
        }

        public async Task SetWebhookAsync() {
            ICollection<EventType> eventTypes = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            var result = await viberBotClient.SetWebhookAsync(viberBotOptions.WebApiEndpoint, eventTypes);
            return;
        }

        public async Task GetUserDetailsAsync(string userId) {
            var result = await viberBotClient.GetUserDetailsAsync(userId);
            return;
        }

        public async Task SendTextMessageAsync(string text, string userId, string trackingData = null) {
            var result = await viberBotClient.SendTextMessageAsync(new TextMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Text = text,
                TrackingData = trackingData
            });
            return;
        }

        public async Task SendBroadcastMessageAsync(string text, IList<string> usersId) {
            var result = await viberBotClient.SendBroadcastMessageAsync(new BroadcastMessage {
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Text = text,
                BroadcastList = usersId
            });
            return;
        }

        public async Task SendPictureMessageAsync(string media, string userId) {
            var result = await viberBotClient.SendPictureMessageAsync(new PictureMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Media = media
            });
            return;
        }

        public async Task SendKeyboardMessageAsync(string userId, string messageText, string buttonText, string actions, string trackingData) {
            var result = await viberBotClient.SendKeyboardMessageAsync(new KeyboardMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Text = messageText,
                Keyboard = new Keyboard {
                    Buttons = new[]
                    {
                        new KeyboardButton
                        {
                            Text = buttonText,
                            ActionBody = actions
                        }
                    }
                },
                TrackingData = trackingData
            });
            return;
        }
    }
}
