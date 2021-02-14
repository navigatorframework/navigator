using System;

namespace Navigator.Provider.Telegram
{
    public static class NavigatorProviderConfigurationExtension
    {
        public static NavigatorConfiguration Telegram(this NavigatorProviderConfiguration providerConfiguration,
            Action<NavigatorTelegramProviderOptions> options)
        {
            var telegramProviderOptions = new NavigatorTelegramProviderOptions();
            options.Invoke(telegramProviderOptions);

            return providerConfiguration.Provider(
                optionsAction => optionsAction.Import(optionsAction.RetrieveAllOptions()), 
                null);
        }
    }
}