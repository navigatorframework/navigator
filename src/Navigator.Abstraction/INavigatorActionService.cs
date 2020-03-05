using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorActionService
    {
        Task Sail(Message message);
        Task Sail(CallbackQuery callbackQuery);
        Task Sail(InlineQuery inlineQuery);
        Task Sail(ChosenInlineResult chosenInlineResult);
        Task Sail(Update update);
    }
}