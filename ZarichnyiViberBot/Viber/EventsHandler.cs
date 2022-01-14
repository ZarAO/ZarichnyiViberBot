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

        public string GetActionType(string trackingData) {
            if (trackingData.Substring(0, 3) == "top") {
                return trackingData.Substring(0, 3);
            }
            return trackingData;
        }

        public async Task IncorectMessage(string senderId) {
            await _viberBot.SendKeyboardMessageAsync(senderId, Strings.STR_UnidentifiedMessage, Strings.STR_GETACTIVITYINFO,
                Strings.STR_action_GETACTIVITYINFO, Strings.STR_track_GETACTIVITYINFO);
        }

        public async Task onReceiveMessage(string senderId, MessageBase message) {
            if (message.Type == MessageType.Text) {
                TextMessage textMessage = (TextMessage) message;
                if (String.IsNullOrEmpty(message.TrackingData)) {
                    await IncorectMessage(senderId);
                } else {
                    switch (GetActionType(message.TrackingData)) {
                        case "IMEI": {
                            await _viberBot.SendKeyboardMessageAsync(senderId, Strings.STR_action_IMEI,
                                Strings.STR_GETTO10, Strings.STR_action_GETTO10, Strings.STR_track_GETTO10);
                            break;
                        }
                        case "get": {
                            if (textMessage.Text != Strings.STR_action_GETACTIVITYINFO) {
                                await IncorectMessage(senderId);
                                break;
                            }
                            await _viberBot.SendTextMessageAsync(textMessage.Text, senderId, Strings.STR_track_IMEI);
                            break;
                        }
                        case "top": {
                            if (textMessage.Text != Strings.STR_action_GETTO10) {
                                await IncorectMessage(senderId);
                                break;
                            }
                            await _viberBot.SendKeyboardMessageAsync(senderId, textMessage.Text,
                                Strings.STR_GOBACK, Strings.STR_action_GOBACK, Strings.STR_track_GOBACK);
                            break;
                        }
                        case "back": {
                            if (textMessage.Text != Strings.STR_action_GOBACK) {
                                await IncorectMessage(senderId);
                                break;
                            }
                            await _viberBot.SendKeyboardMessageAsync(senderId, textMessage.Text,
                                Strings.STR_GETACTIVITYINFO, Strings.STR_action_GETACTIVITYINFO, Strings.STR_track_GETACTIVITYINFO);
                            break;
                        }
                    }
                }
            } else {
                await _viberBot.SendKeyboardMessageAsync(senderId, Strings.STR_UnidentifiedMessageType + Strings.STR_UnidentifiedMessage,
                    Strings.STR_GETACTIVITYINFO, Strings.STR_action_GETACTIVITYINFO, Strings.STR_track_GETACTIVITYINFO);
            }
            
        }

        public async Task onSubscribed(string userId) {
            await _viberBot.SendKeyboardMessageAsync(userId, Strings.STR_SUBSCRIBED, Strings.STR_GETACTIVITYINFO,
                Strings.STR_action_GETACTIVITYINFO, Strings.STR_track_GETACTIVITYINFO);
        }

        public async Task onConversationStarted(string userId, string userName, string userAvatarUrl) {
            string welcomeMessage = String.Format(Strings.STR_WELCOME, userName);
            await _viberBot.SendKeyboardMessageAsync(userId, welcomeMessage, Strings.STR_GETACTIVITYINFO, 
                Strings.STR_action_GETACTIVITYINFO, Strings.STR_track_GETACTIVITYINFO);
            
        }
    }
}
