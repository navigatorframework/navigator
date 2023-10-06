using Microsoft.Extensions.DependencyInjection;

namespace Navigator.Configuration.Provider;

/// <summary>
/// Provides a entry point for configuring new providers for Navigator.
/// </summary>
public class NavigatorProviderConfiguration
{
    private readonly NavigatorConfiguration _navigatorConfiguration;

    /// <summary>
    /// Default constructor for <see cref="NavigatorProviderConfiguration"/>.
    /// </summary>
    /// <param name="navigatorConfiguration"></param>
    public NavigatorProviderConfiguration(NavigatorConfiguration navigatorConfiguration)
    {
        _navigatorConfiguration = navigatorConfiguration;
    }

    /// <summary>
    /// Configure a new provider using this method.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public NavigatorConfiguration Provider(Action<(NavigatorOptions Options, IServiceCollection Services)> configuration)
    {
        configuration.Invoke((_navigatorConfiguration.Options, _navigatorConfiguration.Services));
        
        _navigatorConfiguration.RegisterOrReplaceOptions();
            
        return _navigatorConfiguration;
    }
}