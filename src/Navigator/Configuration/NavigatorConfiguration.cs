using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Navigator.Configuration.Extension;
using Navigator.Configuration.Provider;

namespace Navigator.Configuration;

/// <summary>
/// Helper functions for configuring navigator services.
/// </summary>
public class NavigatorConfiguration
{
    public NavigatorProviderConfiguration WithProvider { get; internal set; }
        
    public NavigatorExtensionConfiguration WithExtension { get; internal set; }

    /// <summary>
    /// Gets the <see cref="NavigatorOptions"/> that are being used.
    /// </summary>
    /// <value>
    /// The <see cref="NavigatorOptions"/> 
    /// </value>
    public NavigatorOptions Options { get; internal set; }
        
    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection"/> services are attached to.
    /// </value>
    public IServiceCollection Services { get; internal set; }

    /// <summary>
    /// Creates a new instance of <see cref="NavigatorConfiguration"/>.
    /// </summary>
    /// <param name="options">The <see cref="NavigatorOptions"/> to use.</param>
    /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
    public NavigatorConfiguration(Action<NavigatorOptions> options, IServiceCollection services)
    {
        Options = new NavigatorOptions();
        options.Invoke(Options);
            
        Services = services;

        services.AddSingleton(Options);
            
        WithProvider = new NavigatorProviderConfiguration(this);
        WithExtension = new NavigatorExtensionConfiguration(this);
    }
        

    public void RegisterOrReplaceOptions()
    {
        Services.Replace(ServiceDescriptor.Singleton<INavigatorOptions, NavigatorOptions>(_ => Options));
    }
}