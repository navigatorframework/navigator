using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Configuration.Options;

/// <summary>
/// Navigator Configuration Options.
/// </summary>
public static class NavigatorOptionsCollectionExtensions
{
    #region WebHookBaseUrl

    private const string WebHookBaseUrlKey = "_navigator.options.webhook_base_url";

    /// <summary>
    /// Sets the webhook base url.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <param name="webHookBaseUrl"></param>
    public static void SetWebHookBaseUrl(this NavigatorOptions navigatorOptions, string webHookBaseUrl)
    {
        navigatorOptions.TryRegisterOption(WebHookBaseUrlKey, webHookBaseUrl);
    }

    /// <summary>
    /// Retrieves the webhook base url
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static string? GetWebHookBaseUrl(this INavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<string>(WebHookBaseUrlKey);
    }

    #endregion

    #region WebHookEndpoint

    private const string WebHookEndpointKey = "_navigator.options.webhook_endpoint";

    /// <summary>
    /// Sets the webhook endpoint.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <param name="webHookEndpoint"></param>
    public static void SetWebHookEndpoint(this NavigatorOptions navigatorOptions, string webHookEndpoint)
    {
        navigatorOptions.TryRegisterOption(WebHookEndpointKey, webHookEndpoint);
    }

    /// <summary>
    /// Retrieves the webhook endpoint or a default one if not set.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static string GetWebHookEndpointOrDefault(this INavigatorOptions navigatorOptions)
    {
        var webHookEndpoint = navigatorOptions.RetrieveOption<string>(WebHookEndpointKey);

        if (webHookEndpoint is null)
        {
            navigatorOptions.TryRegisterOption(WebHookEndpointKey, $"telegram/bot/{Guid.NewGuid()}");
        }

        return navigatorOptions.RetrieveOption<string>(WebHookEndpointKey)!;
    }

    #endregion

    #region MultipleActions

    private const string MultipleActionsKey = "_navigator.options.multiple_actions";

    /// <summary>
    /// Enables the usage of multiple actions for one <see cref="Update"/>
    /// </summary>
    /// <param name="navigatorOptions"></param>
    public static void EnableMultipleActionsUsage(this NavigatorOptions navigatorOptions)
    {
        navigatorOptions.TryRegisterOption(MultipleActionsKey, true);
    }

    /// <summary>
    /// Returns true if the usage of multiple actions for one <see cref="Update"/> is enabled.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static bool MultipleActionsUsageIsEnabled(this INavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<bool>(MultipleActionsKey);
    }

    #endregion

    #region TelegramToken

    private const string TelegramTokenKey = "_navigator.options.telegram_token";

    /// <summary>
    /// Configures de Telegram token.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <param name="telegramToken"></param>
    public static void SetTelegramToken(this NavigatorOptions navigatorOptions, string telegramToken)
    {
        navigatorOptions.TryRegisterOption(TelegramTokenKey, telegramToken);
    }

    /// <summary>
    /// Retrieves the Telegram token.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static string? GetTelegramToken(this INavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<string>(TelegramTokenKey);
    }

    #endregion
    
    #region ChatActionNotification

    private const string ChatActionNotificationKey = "_navigator.options.chataction_notification";

    /// <summary>
    /// Enables the sending of <see cref="ChatAction"/>.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    public static void EnableChatActionNotification(this NavigatorOptions navigatorOptions)
    {
        navigatorOptions.TryRegisterOption(ChatActionNotificationKey, true);
    }

    /// <summary>
    /// Returns true if the sending of <see cref="ChatAction"/> is enabled.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static bool ChatActionNotificationIsEnabled(this INavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<bool>(ChatActionNotificationKey);
    }

    #endregion
}