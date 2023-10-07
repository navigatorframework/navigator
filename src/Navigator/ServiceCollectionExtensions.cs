using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Actions;
using Navigator.Configuration;
using Navigator.Context;
using Navigator.Context.Accessor;
using Navigator.Context.Builder;
using Navigator.Extensions;
using Navigator.Extensions.Bundled.OriginalEvent;
using Scrutor;

namespace Navigator;

public static class ServiceCollectionExtensions
{
    public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
        }

        var navigatorBuilder = new NavigatorConfiguration(options, services);

        services.AddNavigatorContextServices();
        
        services.AddScoped<TelegramMiddleware>();
        
        services.AddScoped<INavigatorContextExtension, OriginalEventContextExtension>();

        services.AddScoped<IActionLauncher, ActionLauncher>();

        services.AddMediatR(navigatorBuilder.Options.GetActionsAssemblies());

        services.Scan(scan => scan
            .FromAssemblies(navigatorBuilder.Options.GetActionsAssemblies())
            .AddClasses(classes => classes.AssignableTo<IAction>())
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .AsSelf()
            .WithScopedLifetime());

        navigatorBuilder.Options.RegisterActionsCore(services
            .Where(descriptor => descriptor.ImplementationType?.IsAssignableTo(typeof(IAction)) ?? false)
            .Select(descriptor => descriptor.ImplementationType!));
        
        navigatorBuilder.Options.RegisterActionsPriorityCore(services
            .Where(descriptor => descriptor.ImplementationType?.IsAssignableTo(typeof(IAction)) ?? false)
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