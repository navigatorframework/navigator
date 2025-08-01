using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.Store.Persistence.Context;
using Navigator.Extensions.Store.Steps;

namespace Navigator.Extensions.Store;

public class StoreExtension : INavigatorExtension<StoreOptions>
{
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, StoreOptions extensionOptions)
    {
        extensionOptions.RunDbContextConfiguration(services);
        
        services.AddScoped<INavigatorPipelineStep, RegisterConversationStep>();
    }
}

public class StoreOptions : INavigatorExtensionOptions
{
    private Action<IServiceCollection>? ConfigureDbContext { get; set; }

    public void ConfigureStore(Action<DbContextOptionsBuilder> options)
    {
        ConfigureStore<NavigatorStoreDbContext>(options);
    }
    
    public void ConfigureStore<TDbContext>(Action<DbContextOptionsBuilder> options) where TDbContext : NavigatorStoreDbContext
    {
        ConfigureDbContext = serviceCollection =>
        {
            serviceCollection.AddDbContext<TDbContext>(options);
        };
    }

    internal void RunDbContextConfiguration(IServiceCollection services)
    {
        ConfigureDbContext?.Invoke(services);
    }
}