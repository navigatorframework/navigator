using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Strategies;
using Navigator.Configuration.Options;

namespace Navigator.Configuration;

/// <summary>
///     Helper functions for configuring navigator services.
/// </summary>
public class NavigatorConfiguration
{
    private readonly List<(Type, object?)> _extensions = [];
    private (Type, object?)? _strategy;

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
    ///     Returns true if a custom strategy package has been configured.
    /// </summary>
    internal bool HasStrategy => _strategy is not null;

    /// <summary>
    ///     Registers a strategy package without any specific options configuration.
    ///     Only one strategy can be active; calling this again replaces the previous one.
    /// </summary>
    /// <typeparam name="TStrategy">The strategy definition type.</typeparam>
    public void WithStrategy<TStrategy>()
        where TStrategy : INavigatorStrategyDefinition
    {
        _strategy = (typeof(TStrategy), null);
    }

    /// <summary>
    ///     Registers a strategy package with configuration for its options.
    ///     Only one strategy can be active; calling this again replaces the previous one.
    /// </summary>
    /// <typeparam name="TStrategy">The strategy definition type.</typeparam>
    /// <typeparam name="TOptions">The strategy options type.</typeparam>
    /// <param name="configure">Action to configure the strategy options.</param>
    public void WithStrategy<TStrategy, TOptions>(Action<TOptions> configure)
        where TStrategy : INavigatorStrategyDefinition<TOptions>
        where TOptions : class, INavigatorStrategyOptions, new()
    {
        var options = new TOptions();
        configure(options);
        _strategy = (typeof(TStrategy), options);
    }

    /// <summary>
    ///     Registers an extension without any specific options configuration.
    /// </summary>
    /// <typeparam name="TExtension">The extension type.</typeparam>
    public void WithExtension<TExtension>()
        where TExtension : INavigatorExtension
    {
        _extensions.Add((typeof(TExtension), null));
    }

    /// <summary>
    ///     Registers an extension with configuration for its options.
    /// </summary>
    /// <typeparam name="TExtension">The extension type.</typeparam>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="configure">Action to configure the extension options.</param>
    public void WithExtension<TExtension, TOptions>(Action<TOptions> configure)
        where TExtension : INavigatorExtension<TOptions>
        where TOptions : class, INavigatorExtensionOptions, new()
    {
        var options = new TOptions();
        configure(options);

        Options.SetExtensionOptions<TExtension, TOptions>(options);

        _extensions.Add((typeof(TExtension), options));
    }

    internal void Configure(IServiceCollection services)
    {
        ConfigureStrategy(services);
        ConfigureExtensions(services);
    }

    private void ConfigureStrategy(IServiceCollection services)
    {
        if (_strategy is not { } strategyEntry) return;

        var (strategyType, options) = strategyEntry;
        var instance = Activator.CreateInstance(strategyType);

        if (options is not null)
        {
            var genericInterface = strategyType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType &&
                                     i.GetGenericTypeDefinition() == typeof(INavigatorStrategyDefinition<>));

            if (genericInterface is not null)
            {
                var configureMethod = genericInterface.GetMethod("Configure");
                configureMethod?.Invoke(instance, [services, Options, options]);
                return;
            }
        }

        if (instance is INavigatorStrategyDefinition nonGenericStrategy)
        {
            nonGenericStrategy.Configure(services, Options);
        }
    }

    private void ConfigureExtensions(IServiceCollection services)
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

            if (extension is INavigatorExtension nonGenericExtension)
            {
                nonGenericExtension.Configure(services, Options);
            }
        }
    }
}
