using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;
using System.Reflection;

namespace Navigator.Configuration;

/// <summary>
///     Helper functions for configuring navigator services.
/// </summary>
public class NavigatorConfiguration
{
    private readonly List<(Type, object?)> _extensions = [];

    /// <summary>
    ///     Gets the <see cref="NavigatorOptions" /> that are being used.
    /// </summary>
    /// <value>
    ///     The <see cref="NavigatorOptions" />
    /// </value>
    public readonly NavigatorOptions Options;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public NavigatorConfiguration()
    {
        Options = new NavigatorOptions();
    }

    /// <summary>
    /// Registers an extension without any specific options configuration
    /// </summary>
    /// <typeparam name="TExtension">The extension type</typeparam>
    public void WithExtension<TExtension>()
        where TExtension : INavigatorExtension
    {
        _extensions.Add((typeof(TExtension), null));
    }

    /// <summary>
    /// Registers an extension with configuration for its options
    /// </summary>
    /// <typeparam name="TExtension">The extension type</typeparam>
    /// <typeparam name="TOptions">The options type</typeparam>
    /// <param name="configure">Action to configure the extension options</param>
    public void WithExtension<TExtension, TOptions>(Action<TOptions> configure)
        where TExtension : INavigatorExtension<TOptions>
        where TOptions : class, INavigatorExtensionOptions, new()
    {
        var options = new TOptions();
        configure(options);

        // Store the configured options in the global NavigatorOptions
        Options.SetExtensionOptions<TExtension, TOptions>(options);

        _extensions.Add((typeof(TExtension), options));
    }

    internal void Configure(IServiceCollection services)
    {
        foreach (var (extensionType, options) in _extensions)
        {
            var extension = Activator.CreateInstance(extensionType);
            
            if (options != null)
            {
                var genericInterface = extensionType.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && 
                                       i.GetGenericTypeDefinition() == typeof(INavigatorExtension<>));
                
                if (genericInterface != null)
                {
                    var configureMethod = genericInterface.GetMethod("Configure");
                    if (configureMethod != null)
                    {
                        configureMethod.Invoke(extension, [services, Options, options]);
                        continue;
                    }
                }
            }
            
            // Check if this implements INavigatorExtension (non-generic)
            if (extension is INavigatorExtension nonGenericExtension)
            {
                nonGenericExtension.Configure(services, Options);
            }
        }
    }
}