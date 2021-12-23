using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extension;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Bundled;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store;

public static class NavigatorExtensionConfigurationExtensions
{
    public static NavigatorConfiguration Store(this NavigatorExtensionConfiguration providerConfiguration, Action<DbContextOptionsBuilder>? dbContextOptions = default)
    {
        return providerConfiguration.Extension(
            _ => {},
            services =>
            {
                services.AddDbContext<NavigatorDbContext>(dbContextOptions);
                services.AddScoped<INavigatorContextExtension, StoreContextExtension>();
                services.AddScoped<INavigatorContextExtension, UniversalConversationContextExtension>();
                services.AddTransient<IUniversalStore, UniversalStore>();
            });
    }
}