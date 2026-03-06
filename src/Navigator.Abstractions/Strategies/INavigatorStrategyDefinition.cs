using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration.Options;

namespace Navigator.Abstractions.Strategies;

/// <summary>
///     Defines a Navigator strategy package that can register its own
///     <see cref="INavigatorStrategy" /> implementation, hosted services,
///     and any supporting infrastructure.
/// </summary>
/// <remarks>
///     Use this interface when your strategy does not require custom configuration options.
/// </remarks>
public interface INavigatorStrategyDefinition
{
    /// <summary>
    ///     Configures the services for this strategy package.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="options">The global Navigator configuration options.</param>
    void Configure(IServiceCollection services, NavigatorOptions options);
}

/// <summary>
///     Defines a Navigator strategy package that can register its own
///     <see cref="INavigatorStrategy" /> implementation with package-specific options.
/// </summary>
/// <typeparam name="TOptions">
///     The type of strategy-specific options that must implement
///     <see cref="INavigatorStrategyOptions" />.
/// </typeparam>
public interface INavigatorStrategyDefinition<in TOptions> where TOptions : INavigatorStrategyOptions
{
    /// <summary>
    ///     Configures the services for this strategy package with strategy-specific options.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="navigatorOptions">The global Navigator configuration options.</param>
    /// <param name="strategyOptions">The strategy-specific configuration options.</param>
    void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, TOptions strategyOptions);
}
