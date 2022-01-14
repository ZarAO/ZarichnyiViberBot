namespace ViberBotServer.Viber
{
    public class Strings
    {
        public static readonly string STR_WELCOME = "Приветствую, {0}!";
        public static readonly string STR_BADREQUEST = "IMEI не найден. Пожалуйста, введите IMEI повторно";
        public static readonly string STR_SUBSCRIBED = "С возвращением!";
        public static readonly string STR_GETACTIVITYINFO = "Получить информацию о активности";
        public static readonly string STR_GETTO10 = "ТОП 10 прогулок";
        public static readonly string STR_GOBACK = "Назад";
        public static readonly string STR_action_IMEI = "Всего прогулок: {0},\nВсего км пройдено: {1},\nВсего времени, мин: {2}\n\nВсего прогулок за сегодня: {3},\nВсего км пройдено за сегодня: {4},\nВсего времени за сегодня, мин: {5}";
        public static readonly string STR_track_IMEI = "IMEI";
        public static readonly string STR_action_GETACTIVITYINFO = "Введите IMEI";
        public static readonly string STR_action_GETTO10 = "ТОП 10 самых долгих прогулок";
        public static readonly string STR_action_GOBACK = "Выберете действие из меню";
        public static readonly string STR_track_GETACTIVITYINFO = "get";
        public static readonly string STR_track_GETTO10 = "top_{0}";
        public static readonly string STR_track_GOBACK = "back";
        public static readonly string STR_UnidentifiedMessage = "Пожалуйста, воспользуйтесь меню";
        public static readonly string STR_UnidentifiedMessageType = "К сожалению, данный тип сообщения не поддерживаеться ботом. ";


        public static readonly string STR_HTMLTableTitleTemplate = "|   №   |   Км  |   Мин |";
        public static readonly string STR_HTMLTablerowTemplate = "|{0}|{1}|{2}|";

    }
}
