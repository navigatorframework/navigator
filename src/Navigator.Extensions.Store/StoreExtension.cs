using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
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