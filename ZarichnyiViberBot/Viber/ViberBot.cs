using System;
using System.Linq;
using System.Collections.Generic;
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

        public async Task SendTextMessageAsync(string text, string userId) {
            var result = await viberBotClient.SendTextMessageAsync(new TextMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Text = text
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

        public async Task SendContactMessageAsync(string userId) {
            var result = await viberBotClient.SendContactMessageAsync(new ContactMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Contact = new Contact {
                    Name = "Brad Pitt",
                    TN = "+0123456789"
                }
            });
            return;
        }

        public async Task SendFileMessageAsync(string userId) {
            var result = await viberBotClient.SendFileMessageAsync(new FileMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Media = "http://www.sample-videos.com/audio/mp3/crowd-cheering.mp3",
                FileName = "audio.mp3",
                Size = 443926
            });
            return;
        }

        public async Task SendLocationMessageAsync(string userId) {
            var result = await viberBotClient.SendLocationMessageAsync(new LocationMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Location = new Location {
                    Lon = 1,
                    Lat = -2
                }
            });
            return;
        }

        public async Task SendStickerMessageAsync(string userId) {
            var result = await viberBotClient.SendStickerMessageAsync(new StickerMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                StickerId = "40108"
            });
            return;
        }

        public async Task SendVideoMessageAsync(string userId) {
            var result = await viberBotClient.SendVideoMessageAsync(new VideoMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Media = "https://github.com/mediaelement/mediaelement-files/blob/master/big_buck_bunny.mp4?raw=true",
                Size = 5510872
            });
            return;
        }

        public async Task SendUrlMessageAsync(string userId) {
            var result = await viberBotClient.SendUrlMessageAsync(new UrlMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Media = "https://www.google.com"
            });
            return;
        }

        public async Task SendKeyboardMessageAsync(string userId) {
            var result = await viberBotClient.SendKeyboardMessageAsync(new KeyboardMessage {
                Receiver = userId,
                Sender = new UserBase {
                    Name = this.viberBotOptions.ViberBotName
                },
                Text = "Test keyboard",
                Keyboard = new Keyboard {
                    Buttons = new[]
                    {
                        new KeyboardButton
                        {
                            Text = "Button 1",
                            ActionBody = "AB1"
                        }
                    }
                },
                TrackingData = "td"
            });
            return;
        }
    }
}
