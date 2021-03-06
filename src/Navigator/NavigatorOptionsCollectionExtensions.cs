using System;
using System.Collections.Generic;
using System.Reflection;
using MihaZupan.TelegramBotClients.RateLimitedClient;

namespace Navigator
{
    /// <summary>
    /// 
    /// </summary>
    public static class NavigatorOptionsCollectionExtensions
    {
        #region SchedulerSettings

        private const string SchedulerSettingsKey = "_navigator.options.scheduler_settings";

        public static void SetSchedulerSettings(this NavigatorOptions navigatorOptions, SchedulerSettings schedulerSettings)
        {
            navigatorOptions.TryRegisterOption(SchedulerSettingsKey, schedulerSettings);
        }

        public static SchedulerSettings GetSchedulerSettingsOrDefault(this NavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<SchedulerSettings>(SchedulerSettingsKey) ?? SchedulerSettings.Default;
        }

        #endregion
        
        #region TelegramToken

        private const string TelegramTokenKey = "_navigator.options.telegram_token";

        public static void SetTelegramToken(this NavigatorOptions navigatorOptions, string telegramToken)
        {
            navigatorOptions.TryRegisterOption(TelegramTokenKey, telegramToken);

        }

        public static string? GetTelegramToken(this NavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<string>(TelegramTokenKey);
        }
        
        #endregion
        
        #region WebHookBaseUrl

        private const string WebHookBaseUrlKey = "_navigator.options.webhook_base_url";

        public static void SetWebHookBaseUrl(this NavigatorOptions navigatorOptions, string webHookBaseUrl)
        {
            navigatorOptions.TryRegisterOption(WebHookBaseUrlKey, webHookBaseUrl);

        }

        public static string? GetWebHookBaseUrl(this NavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<string>(WebHookBaseUrlKey);
        }
        
        #endregion
        
        #region WebHookEndpoint

        private const string WebHookEndpointKey = "_navigator.options.webhook_endpoint";

        public static void SetWebHookEndpoint(this NavigatorOptions navigatorOptions, string webHookEndpoint)
        {
            navigatorOptions.TryRegisterOption(WebHookEndpointKey, webHookEndpoint);
        }

        public static string GetWebHookEndpointOrDefault(this NavigatorOptions navigatorOptions)
        {
            var webHookEndpoint = navigatorOptions.RetrieveOption<string>(WebHookEndpointKey);

            if (webHookEndpoint is null)
            {
                navigatorOptions.SetWebHookEndpoint($"bot/{Guid.NewGuid()}");
            }
            
            return navigatorOptions.RetrieveOption<string>(WebHookEndpointKey)!;
        }

        #endregion

        #region MultipleActions

        private const string MultipleActionsKey = "_navigator.options.multiple_actions";

        public static void EnableMultipleActionsUsage(this NavigatorOptions navigatorOptions)
        {
            navigatorOptions.TryRegisterOption(MultipleActionsKey, true);
        }

        public static bool MultipleActionsUsageIsEnabled(this NavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<bool>(MultipleActionsKey);
        }

        #endregion
        
        #region ActionsAssemblies

        private const string ActionsAssembliesKey = "_navigator.options.actions_assemblies";

        public static void RegisterActionsFromAssemblies(this NavigatorOptions navigatorOptions, params Assembly[] assemblies)
        {
            var registeredAssemblies = navigatorOptions.RetrieveOption<Assembly[]>(ActionsAssembliesKey);

            if (registeredAssemblies?.Length > 0)
            {
                var combinedAssemblies = new List<Assembly>(registeredAssemblies);
                combinedAssemblies.AddRange(assemblies);
                
                navigatorOptions.TryRegisterOption(ActionsAssembliesKey, combinedAssemblies);
            }
            else
            {
                navigatorOptions.TryRegisterOption(ActionsAssembliesKey, assemblies);
            }
        }

        public static Assembly[] GetActionsAssemblies(this NavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<Assembly[]>(ActionsAssembliesKey) ?? new[] { Assembly.GetCallingAssembly()};
        }

        #endregion
    }
}