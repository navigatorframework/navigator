using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;
using Navigator.Extensions.Management.Services;

namespace Navigator.Extensions.Management;

/// <summary>
///     Extension for adding management capabilities to Navigator.
/// </summary>
public sealed class ManagementExtension : INavigatorExtension<ManagementOptions>
{
    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, ManagementOptions extensionOptions)
    {
        services.AddSingleton<ITraceFormatter, TraceFormatter>();
    }
}
