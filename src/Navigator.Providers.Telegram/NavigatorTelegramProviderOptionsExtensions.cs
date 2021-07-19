using System;
using Navigator.Configuration;

namespace Navigator.Providers.Telegram
{
    public static class NavigatorTelegramProviderOptionsExtensions
    {
        #region TelegramToken

        private const string TelegramTokenKey = "_navigator.options.telegram.telegram_token";

        public static void SetTelegramToken(this NavigatorTelegramProviderOptions navigatorOptions, string telegramToken)
        {
            navigatorOptions.TryRegisterOption(TelegramTokenKey, telegramToken);

        }

        public static string? GetTelegramToken(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<string>(TelegramTokenKey);
        }
        
        #endregion
        
        #region WebHookEndpoint

        private const string WebHookEndpointKey = "_navigator.options.telegram.webhook_endpoint";

        public static void SetWebHookEndpoint(this NavigatorTelegramProviderOptions navigatorOptions, string webHookEndpoint)
        {
            navigatorOptions.TryRegisterOption(WebHookEndpointKey, webHookEndpoint);
        }

        public static string GetWebHookEndpointOrDefault(this INavigatorOptions navigatorOptions)
        {
            var webHookEndpoint = navigatorOptions.RetrieveOption<string>(WebHookEndpointKey);

            if (webHookEndpoint is null)
            {
                navigatorOptions.TryRegisterOption(WebHookEndpointKey,$"bot/{Guid.NewGuid()}");
            }
            
            return navigatorOptions.RetrieveOption<string>(WebHookEndpointKey)!;
        }

        #endregion
    }
}