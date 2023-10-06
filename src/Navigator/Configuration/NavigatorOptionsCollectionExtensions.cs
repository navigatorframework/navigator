using System.Reflection;

namespace Navigator.Configuration;

/// <summary>
/// Navigator Configuration Options.
/// </summary>
public static class NavigatorOptionsCollectionExtensions
{
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