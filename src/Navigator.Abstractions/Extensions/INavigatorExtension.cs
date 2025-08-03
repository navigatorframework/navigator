using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration.Options;

namespace Navigator.Abstractions.Extensions;

/// <summary>
///     <para>
///         Defines a Navigator framework extension that provides pluggable functionality to the Navigator Telegram bot
///         framework.
///     </para>
///     <para>
///         Extensions implementing this interface can register pipeline steps, services, and other components to extend
///         the framework's capabilities, such as adding cooldowns, probabilities, or custom message processing logic.
///     </para>
///     <para>
///         Common use cases include:
///         <list type="bullet">
///             <item>
///                 <description>Adding pipeline steps for filtering or processing actions</description>
///             </item>
///             <item>
///                 <description>Registering middleware for cross-cutting concerns</description>
///             </item>
///             <item>
///                 <description>Adding simple services that don't require configuration</description>
///             </item>
///         </list>
///     </para>
/// </summary>
/// <remarks>
///     Choose this interface when your extension doesn't need custom configuration parameters.
/// </remarks>
public interface INavigatorExtension
{
    /// <summary>
    ///     Configures the services and options for this Navigator extension.
    /// </summary>
    /// <remarks>
    ///     This method is called during the Navigator framework initialization.
    /// </remarks>
    /// <param name="services">
    ///     The service collection to register services with. Use this to register your extension's
    ///     dependencies.
    /// </param>
    /// <param name="options">The global Navigator configuration options. Access framework-wide settings here.</param>
    void Configure(IServiceCollection services, NavigatorOptions options);
}

/// <summary>
///     <para>
///         Defines a Navigator framework extension that can configure services and options with additional
///         extension-specific
///         configuration.
///     </para>
///     <para>
///         Extensions implementing this interface can register pipeline steps, services, and other components to extend
///         the framework's capabilities, such as adding cooldowns, probabilities, or custom message processing logic.
///     </para>
///     <para>
///         Common use cases include:
///         <list type="bullet">
///             <item>
///                 <description>Adding pipeline steps for filtering or processing actions</description>
///             </item>
///             <item>
///                 <description>Registering middleware for cross-cutting concerns</description>
///             </item>
///             <item>
///                 <description>Adding simple services that don't require configuration</description>
///             </item>
///         </list>
///     </para>
/// </summary>
/// <remarks>
///     Choose this interface when your extension requires custom configuration parameters.
/// </remarks>
/// <typeparam name="TOptions">
///     The type of extension-specific options that must implement
///     <see cref="INavigatorExtensionOptions" />.
/// </typeparam>
public interface INavigatorExtension<in TOptions> where TOptions : INavigatorExtensionOptions
{
    /// <summary>
    ///     Configures the services and options for this Navigator extension with extension-specific configuration.
    /// </summary>
    /// <remarks>
    ///     This method is called during the Navigator framework initialization.
    /// </remarks>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="navigatorOptions">The Navigator configuration options.</param>
    /// <param name="extensionOptions">The extension-specific configuration options.</param>
    void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, TOptions extensionOptions);
}