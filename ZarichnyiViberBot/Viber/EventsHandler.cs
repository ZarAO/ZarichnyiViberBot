using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viber.Bot;
using ZarichnyiViberBot.ViberDBHelper;

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
                            List<WalkAnalytics> data = DBUtils.GetWalkAnalyticsAllTime(textMessage.Text);
                            if (data.Count == 0) {
                                await _viberBot.SendTextMessageAsync(Strings.STR_BADREQUEST, senderId, Strings.STR_track_IMEI);
                                break;
                            }
                            WalkAnalytics walkAnalytics = data.First<WalkAnalytics>();

                            data = DBUtils.GetWalkAnalyticsOneDay(textMessage.Text);
                            if (data.Count == 0) {
                                await _viberBot.SendTextMessageAsync(Strings.STR_BADREQUEST, senderId, Strings.STR_track_IMEI);
                                break;
                            }
                            WalkAnalytics walkAnalyticsOneDay = data.First<WalkAnalytics>();
                            
                            await _viberBot.SendKeyboardMessageAsync(senderId, String.Format(Strings.STR_action_IMEI, walkAnalytics.CountOfWalk,
                                walkAnalytics.TotalDistance, walkAnalytics.TotalTime.TotalMinutes, walkAnalyticsOneDay.CountOfWalk,
                                walkAnalyticsOneDay.TotalDistance, walkAnalyticsOneDay.TotalTime.TotalMinutes
                                ),
                                Strings.STR_GETTO10, Strings.STR_action_GETTO10, String.Format(Strings.STR_track_GETTO10, textMessage.Text));
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
                            string imeiFromTrack = message.TrackingData.Substring(4);
                            string rowsAllTime = "";
                            List<TopWalk> data = DBUtils.GetTopWalkAllTime(imeiFromTrack);
                            if (data.Count != 0) {
                                data.ForEach(value => rowsAllTime += String.Format(Strings.STR_HTMLTablerowTemplate, value.WalkNumber.ToString().PadLeft(7, ' '),
                                    Math.Round(value.Distance, 2).ToString().PadLeft(7, ' '), Math.Round(value.TimeWalk.TotalMinutes, 2).ToString().PadLeft(7, ' ')) +"\n");
                            } 
                            string tableAllTime = String.Format("{0}\n{1}", Strings.STR_HTMLTableTitleTemplate, rowsAllTime);

                            string rowsOneDay = "";
                            data = DBUtils.GetTopWalkOneDay(imeiFromTrack);
                            if (data.Count != 0) {
                                data.ForEach(value => rowsOneDay += String.Format(Strings.STR_HTMLTablerowTemplate, value.WalkNumber.ToString().PadLeft(7, ' '),
                                    Math.Round(value.Distance, 2).ToString().PadLeft(7, ' '), Math.Round(value.TimeWalk.TotalMinutes, 2).ToString().PadLeft(7, ' ')));
                            }
                            string tableOneDay = String.Format("{0}\n{1}", Strings.STR_HTMLTableTitleTemplate, rowsOneDay);

                            await _viberBot.SendTextMessageAsync($"{textMessage.Text}\n\n\nЗа все время: \n{tableAllTime}\nЗа сегодня: \n{tableOneDay}", senderId);

                            await _viberBot.SendKeyboardMessageAsync(senderId, "",
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
