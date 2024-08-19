using Microsoft.Extensions.DependencyInjection;
using Navigator.Bundled.Extensions.Update;
using Navigator.Catalog;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Context;
using Navigator.Context.Accessor;
using Navigator.Context.Builder;
using Navigator.Extensions;
using Navigator.Hosted;
using Scrutor;

namespace Navigator;

/// <summary>
/// Extensions for configuring Navigator.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Navigator.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
        }

        var navigatorBuilder = new NavigatorConfiguration(options, services);

        services.AddNavigatorContextServices();
        
        services.AddScoped<TelegramMiddleware>();

        services.AddScoped<INavigatorClient, NavigatorClient>();
        
        services.AddScoped<INavigatorContextExtension, UpdateNavigatorContextExtension>();

        services.AddSingleton<IBotActionCatalogFactory, BotActionCatalogFactory>();

        services.AddScoped<IActionLauncher, ActionLauncher>();

        services.AddHostedService<SetTelegramBotWebHookHostedService>();
        
        services.Scan(scan => scan
            .FromAssemblies(navigatorBuilder.Options.GetActionsAssemblies())
            .AddClasses(classes => classes.AssignableTo<Action>())
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .AsSelf()
            .WithScopedLifetime());

        navigatorBuilder.Options.RegisterActionsCore(services
            .Where(descriptor => descriptor.ImplementationType?.IsAssignableTo(typeof(Action)) ?? false)
            .Select(descriptor => descriptor.ImplementationType!));
        
        navigatorBuilder.Options.RegisterActionsPriorityCore(services
            .Where(descriptor => descriptor.ImplementationType?.IsAssignableTo(typeof(Action)) ?? false)
            .Select(descriptor => descriptor.ImplementationType!));

        navigatorBuilder.RegisterOrReplaceOptions();

        return navigatorBuilder;
    }

    internal static void AddNavigatorContextServices(this IServiceCollection services)
    {
        services.AddScoped<INavigatorContextBuilder, NavigatorContextBuilder>();
        services.AddScoped<INavigatorContextFactory, NavigatorContextFactory>();
        services.AddTransient<INavigatorContextAccessor, NavigatorContextAccessor>();
    }
}