using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;

namespace Navigator.Extensions.Management;

/// <summary>
///     Extension for adding management capabilities to Navigator.
/// </summary>
public class ManagementExtension : INavigatorExtension<ManagementOptions>
{
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, ManagementOptions extensionOptions)
    {
        throw new NotImplementedException();
    }
}
