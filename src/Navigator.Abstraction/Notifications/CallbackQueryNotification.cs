using MediatR;
using Telegram.Bot.Types;

namespace Navigator.Abstraction.Notifications
{
    public class CallbackQueryNotification : CallbackQuery, INotification
    {
        public new bool IsGameQuery { get; set; }
    }
}