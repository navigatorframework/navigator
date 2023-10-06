using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extension;
using Navigator.Extensions.Store.Bundled;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Context.Extension;

namespace Navigator.Extensions.Store;

public static class NavigatorExtensionConfigurationExtensions
{
    public static NavigatorConfiguration Store(this NavigatorExtensionConfiguration extensionConfiguration, Action<DbContextOptionsBuilder>? dbContextOptions = default)
    {
        var temporal = new DbContextOptionsBuilder();
        
        dbContextOptions?.Invoke(temporal);
        
        return extensionConfiguration.Extension(configuration =>
        {
            
            configuration.Services.AddDbContext<NavigatorDbContext>(dbContextOptions);

            configuration.Services.AddScoped<INavigatorContextExtension, StoreConversationContextExtension>();
            configuration.Services.AddScoped<INavigatorContextExtension, StoreContextExtension>();

            configuration.Services.AddScoped<INavigatorStore, NavigatorStore>();
            
            foreach (var extension in temporal.Options.Extensions.OfType<NavigatorStoreModelExtension>())
            {
                extension.ExtensionServices?.Invoke(configuration.Services);
            }
        });
    }
}