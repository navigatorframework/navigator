using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extension;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Bundled;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Context.Extension;

namespace Navigator.Extensions.Store;

public static class NavigatorExtensionConfigurationExtensions
{
    public static NavigatorConfiguration Store(this NavigatorExtensionConfiguration providerConfiguration, Action<DbContextOptionsBuilder>? dbContextOptions = default)
    {
        var temporal = new DbContextOptionsBuilder();
        
        dbContextOptions?.Invoke(temporal);
        
        return providerConfiguration.Extension(
            _ => {},
            services =>
            {
                services.AddDbContext<NavigatorDbContext>(dbContextOptions);

                services.AddScoped<INavigatorContextExtension, StoreConversationContextExtension>();
                
                foreach (var extension in temporal.Options.Extensions.OfType<NavigatorStoreModelExtension>())
                {
                    extension.ExtensionServices?.Invoke(services);
                }
            });
    }
}