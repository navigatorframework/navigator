using System;
using System.Threading.Tasks;
using Navigator.Providers.Telegram.Entities;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram;

internal class TelegramNavigatorProvider : INavigatorProvider
{
    private readonly NavigatorTelegramClient _client;

    public string Name => "navigator.provider.telegram";

    public TelegramNavigatorProvider(NavigatorTelegramClient client)
    {
        _client = client;
    }
        
    public INavigatorClient GetClient()
    {
        return _client;
    }

    public Type GetConversationType()
    {
        return typeof(TelegramConversation);
    }
}