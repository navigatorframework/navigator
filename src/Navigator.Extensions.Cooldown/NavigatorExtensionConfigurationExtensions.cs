using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extension;

namespace Navigator.Extensions.Cooldown;

/// <summary>
/// Extensions to NavigatorConfiguration
/// </summary>
public static class NavigatorExtensionConfigurationExtensions
{
    /// <summary>
    /// Configures the Cooldown extension.
    /// </summary>
    /// <param name="extensionConfiguration"></param>
    /// <returns></returns>
    public static NavigatorConfiguration Cooldown(this NavigatorExtensionConfiguration extensionConfiguration)
    {

        return extensionConfiguration.Extension(
            configuration =>
            {
                configuration.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CooldownActionMiddleware<,>));
            });
    }
}