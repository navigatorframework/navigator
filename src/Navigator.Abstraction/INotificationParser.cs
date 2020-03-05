using System.Threading.Tasks;
using MediatR;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INotificationParser
    {
        Task<INotification> Parse(Message message);
        Task<INotification> Parse(CallbackQuery callbackQuery);
        Task<INotification> Parse(InlineQuery inlineQuery);
        Task<INotification> Parse(ChosenInlineResult chosenInlineResult);
        Task<INotification> Parse(Update update);
    }
}