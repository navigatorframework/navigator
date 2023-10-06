using Navigator.Providers.Telegram.Entities;

namespace Navigator.Providers.Telegram;

internal class TelegramNavigatorProvider : INavigatorProvider
{
    private readonly NavigatorTelegramClient _client;
    
    public string Name { get; init; } = "navigator.provider.telegram";

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