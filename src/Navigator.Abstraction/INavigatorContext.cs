using System.Collections.Generic;
using System.Threading.Tasks;
using Navigator.Abstraction.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContext
    {
        INavigatorClient Client { get; }
        Message Message { get; }
        InlineQuery InlineQuery { get; }
        CallbackQuery CallbackQuery { get; }
        ChosenInlineResult ChosenInlineResult { get; }
        Update Update { get; }
        Task<TUser> GetUser<TUser>() where TUser : class;
        Task<TChat> GetChat<TChat>() where TChat : class;
        string BotName { get; set; }
        Dictionary<string, INavigatorExtension> Extensions { get; set; }
        Task Populate(CallbackQuery callbackQuery);
        Task Populate(InlineQuery inlineQuery);
        Task Populate(ChosenInlineResult chosenInlineResult);
        Task Populate(Message message);
        Task Populate(Update update);
    }
}    