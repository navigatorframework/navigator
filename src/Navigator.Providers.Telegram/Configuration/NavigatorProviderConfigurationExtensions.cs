using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Provider;
using Navigator.Providers.Telegram.Hosted;

namespace Navigator.Providers.Telegram
{
    public static class NavigatorProviderConfigurationExtensions
    {
        public static NavigatorConfiguration Telegram(this NavigatorProviderConfiguration providerConfiguration,
            Action<NavigatorTelegramProviderOptions> options)
        {
            var telegramProviderOptions = new NavigatorTelegramProviderOptions();
            options.Invoke(telegramProviderOptions);

            return providerConfiguration.Provider(
                optionsAction => optionsAction.Import(telegramProviderOptions.RetrieveAllOptions()),
                services =>
                {
                    services.AddSingleton<NavigatorTelegramClient>();
                    services.AddSingleton<INavigatorClient, NavigatorTelegramClient>(sp => sp.GetRequiredService<NavigatorTelegramClient>());

                    services.AddScoped<INavigatorProvider, TelegramNavigatorProvider>();
                    
                    services.AddScoped<TelegramMiddleware>();
                    
                    services.AddHostedService<SetTelegramBotWebHookHostedService>();

                });
        }
    }
}