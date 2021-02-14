using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions;
using Scrutor;

namespace Navigator
{
    public static class ServiceCollectionExtensions
    {
        public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
            }

            var navigatorBuilder = new NavigatorConfiguration(options, services);

            services.AddSingleton<IBotClient, BotClient>();

            services.AddHostedService<SetTelegramBotWebHookHostedService>();
            services.AddScoped<INotificationLauncher, NotificationLauncher>();
            services.AddScoped<IActionLauncher, ActionLauncher>();
            services.AddScoped<INavigatorContext, NavigatorContext>();
            services.AddScoped<INavigatorContextBuilder, NavigatorContextBuilder>();
            services.AddScoped<INavigatorMiddleware, NavigatorMiddleware>();
            
            services.Scan(scan => scan
                .FromAssemblies(navigatorBuilder.Options.GetActionsAssemblies())
                .AddClasses(classes => classes.AssignableTo<IAction>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            navigatorBuilder.RegisterOrReplaceOptions();
            
            return navigatorBuilder;
        }
    }
}