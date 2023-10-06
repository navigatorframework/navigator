using Microsoft.Extensions.DependencyInjection;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Provider;
using Navigator.Context;
using Navigator.Context.Builder;
using Navigator.Providers.Telegram.Hosted;

namespace Navigator.Providers.Telegram;

public static class NavigatorProviderConfigurationExtensions
{
    public static NavigatorConfiguration Telegram(this NavigatorProviderConfiguration providerConfiguration,
        Action<NavigatorTelegramProviderOptions> options)
    {
        var telegramProviderOptions = new NavigatorTelegramProviderOptions();
        options.Invoke(telegramProviderOptions);

        return providerConfiguration.Provider(configuration =>
        {
            configuration.Options.Import(telegramProviderOptions.RetrieveAllOptions());
           
            configuration.Services.AddSingleton<NavigatorTelegramClient>();
            configuration.Services.AddSingleton<INavigatorClient, NavigatorTelegramClient>(sp => sp.GetRequiredService<NavigatorTelegramClient>());
            configuration.Services.AddScoped<INavigatorProvider, TelegramNavigatorProvider>();
            configuration.Services.AddScoped<INavigatorContextBuilderConversationSource, TelegramNavigatorContextBuilderConversationSource>();
            configuration.Services.AddScoped<TelegramMiddleware>();
            configuration.Services.AddHostedService<SetTelegramBotWebHookHostedService>();
        });
    }
}