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
    /// <param name="configuration"></param>
    /// <returns></returns>
    public NavigatorConfiguration Extension(Action<(NavigatorOptions Options, IServiceCollection Services)> configuration)
    {
        configuration.Invoke((_navigatorConfiguration.Options, _navigatorConfiguration.Services));

        _navigatorConfiguration.RegisterOrReplaceOptions();
            
        return _navigatorConfiguration;
    }
}