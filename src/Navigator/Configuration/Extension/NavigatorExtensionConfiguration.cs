using Microsoft.Extensions.DependencyInjection;

namespace Navigator.Configuration.Extension;

/// <summary>
/// Provides a entry point for configuring new extensions for Navigator.
/// </summary>
public class NavigatorExtensionConfiguration
{
    private readonly NavigatorConfiguration _navigatorConfiguration;

    /// <summary>
    /// Default constructor for <see cref="NavigatorExtensionConfiguration"/>.
    /// </summary>
    /// <param name="navigatorConfiguration"></param>
    public NavigatorExtensionConfiguration(NavigatorConfiguration navigatorConfiguration)
    {
        _navigatorConfiguration = navigatorConfiguration;
    }

    /// <summary>
    /// Configure a new extension using this method.
    /// </summary>
    /// <param name="optionsAction"></param>
    /// <param name="servicesAction"></param>
    /// <returns></returns>
    public NavigatorConfiguration Extension(Action<NavigatorOptions>? optionsAction, Action<IServiceCollection>? servicesAction)
    {
        optionsAction?.Invoke(_navigatorConfiguration.Options);

        servicesAction?.Invoke(_navigatorConfiguration.Services);

        _navigatorConfiguration.RegisterOrReplaceOptions();
            
        return _navigatorConfiguration;
    }
}