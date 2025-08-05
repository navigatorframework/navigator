using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Extensions.Store.Persistence.Context;
using Navigator.Extensions.Store.Resolvers;
using Navigator.Extensions.Store.Services;
using Navigator.Extensions.Store.Steps;

namespace Navigator.Extensions.Store;

public class StoreOptions : INavigatorExtensionOptions
{
    private Action<IServiceCollection>? ConfigureDbContext { get; set; }
    private HybridCacheEntryOptions HybridCacheEntryOptions { get; set; } = new()
    {
        Expiration = TimeSpan.FromMinutes(60),
        LocalCacheExpiration = TimeSpan.FromMinutes(30)
    };

    public void ConfigureStore(Action<DbContextOptionsBuilder> options)
    {
        ConfigureStore<NavigatorStoreDbContext>(options);
    }
    
    public void ConfigureStore<TDbContext>(Action<DbContextOptionsBuilder> options) 
        where TDbContext : NavigatorStoreDbContext
    {
        ConfigureDbContext = serviceCollection =>
        {
            serviceCollection.AddDbContext<TDbContext>(options); 
            serviceCollection.AddScoped<INavigatorPipelineStep, RegisterConversationStep<TDbContext>>();
        
            serviceCollection.AddScoped<IArgumentResolver, StoreArgumentResolver<TDbContext>>();
            
            serviceCollection.AddScoped<INavigatorStore<TDbContext>, NavigatorStore<TDbContext>>();

            if (typeof(TDbContext) == typeof(NavigatorStoreDbContext))
            {
                serviceCollection.AddScoped<INavigatorStore, NavigatorStore>();
            }
        };
    }

    public void ConfigureHybridCache(Action<HybridCacheEntryOptions> cacheEntryOptions)
    {
        cacheEntryOptions(HybridCacheEntryOptions);
    }

    internal void RunDbContextConfiguration(IServiceCollection services)
    {
        ConfigureDbContext?.Invoke(services);
    }
    
    internal HybridCacheEntryOptions RetrieveHybridCacheEntryOptions()
    {
        return HybridCacheEntryOptions;
    }
}