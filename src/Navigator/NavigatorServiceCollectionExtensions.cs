using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstraction;
using Navigator.Actions.Abstraction;
using Navigator.Configuration;
using Navigator.Hosted;
using Scrutor;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static IServiceCollection AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options, params Assembly[] actionAssemblies)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
            }
            
            services.Configure(options);
            
            services.AddSingleton<IBotClient, BotClient>();

            services.AddHostedService<SetTelegramBotWebHookHostedService>();
            services.AddScoped<INotificationLauncher, NotificationLauncher>();
            services.AddScoped<IActionLauncher, ActionLauncher>();
            services.AddScoped<INavigatorContext, NavigatorContext>();
            services.AddScoped<INavigatorContextBuilder, NavigatorContextBuilder>();
            services.AddScoped<INavigatorMiddleware, NavigatorMiddleware>();
            
            services.Scan(scan => scan
                .FromAssemblies(actionAssemblies)
                .AddClasses(classes => classes.AssignableTo<IAction>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
    }
}