using Navigator.Actions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram;

public static class ActionHandlerExtensions
{
    public static NavigatorTelegramClient GetTelegramClient<T>(this ActionHandler<T> actionHandler) where T : IAction
    {
        return actionHandler.NavigatorContext.Provider.GetTelegramClient();
    }
    
    public static Chat GetTelegramChat<T>(this ActionHandler<T> actionHandler) where T : IAction
    {
        return actionHandler.NavigatorContext.GetTelegramChat();
    }
}