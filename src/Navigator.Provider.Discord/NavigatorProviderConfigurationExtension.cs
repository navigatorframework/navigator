using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Provider;
using Navigator.Provider.Telegram.Hosted;

namespace Navigator.Provider.Discord
{
    public static class NavigatorProviderConfigurationExtensions
    {
        public static NavigatorConfiguration Discord(this NavigatorProviderConfiguration providerConfiguration,
            Action<NavigatorDiscordProviderOptions> options)
        {
            var discordProviderOptions = new NavigatorDiscordProviderOptions();
            options.Invoke(discordProviderOptions);

            return providerConfiguration.Provider(
                optionsAction => optionsAction.Import(optionsAction.RetrieveAllOptions()),
                services =>
                {
                    services.AddSingleton<NavigatorDiscordClient>();
                    services.AddSingleton<INavigatorClient, NavigatorDiscordClient>(sp => sp.GetRequiredService<NavigatorDiscordClient>());

                    services.AddHostedService<SetDiscordShardedSocketHostedService>();
                });
        }
    }
}