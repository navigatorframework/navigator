using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extensions;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store;

/// <summary>
/// Extensions for configuring the store in Navigator.
/// </summary>
public static class NavigatorExtensionConfigurationExtensions
{
    /// <summary>
    /// Configure the navigator store.
    /// </summary>
    /// <param name="extensionConfiguration"></param>
    /// <param name="dbContextOptions"></param>
    /// <returns></returns>
    public static NavigatorConfiguration Store(this NavigatorExtensionConfiguration extensionConfiguration, Action<DbContextOptionsBuilder>? dbContextOptions = default)
    {
        var temporal = new DbContextOptionsBuilder();
        
        dbContextOptions?.Invoke(temporal);
        
        return extensionConfiguration.Extension(configuration =>
        {
            configuration.Services.AddDbContext<NavigatorDbContext>(dbContextOptions);

            configuration.Services.AddScoped<INavigatorContextExtension, StoreConversationNavigatorContextExtension>();
        });
    }
}