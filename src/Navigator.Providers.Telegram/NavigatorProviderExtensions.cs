using System;

namespace Navigator.Providers.Telegram;

public static class NavigatorProviderExtensions
{
    public static NavigatorTelegramClient GetTelegramClient(this INavigatorProvider provider)
    {
        var client = provider.GetTelegramClientOrDefault();

        if (client is not null)
        {
            return client;
        }

        throw new Exception($"Navigator client was not of type {nameof(NavigatorTelegramClient)}")
        {
            Data =
            {
                {nameof(Type), provider.GetClient().GetType()}
            }
        };
    }

    public static NavigatorTelegramClient? GetTelegramClientOrDefault(this INavigatorProvider provider)
    {
        if (provider.GetClient() is NavigatorTelegramClient navigatorTelegramClient)
        {
            return navigatorTelegramClient;
        }

        return default;
    }
}