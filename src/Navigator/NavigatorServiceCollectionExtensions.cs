using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions;
using Navigator.Configuration;
using Navigator.Hosted;
using Scrutor;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static NavigatorBuilder AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
            }

            var invokedOptions = new NavigatorOptions();
            options.Invoke(invokedOptions);

            var navigatorBuilder = new NavigatorBuilder(invokedOptions, services);
            
            services.AddSingleton(invokedOptions);
            
            services.AddSingleton<IBotClient, BotClient>();

            services.AddHostedService<SetTelegramBotWebHookHostedService>();
            services.AddScoped<INotificationLauncher, NotificationLauncher>();
            services.AddScoped<IActionLauncher, ActionLauncher>();
            services.AddScoped<INavigatorContext, NavigatorContext>();
            services.AddScoped<INavigatorContextBuilder, NavigatorContextBuilder>();
            services.AddScoped<INavigatorMiddleware, NavigatorMiddleware>();
            
            services.Scan(scan => scan
                .FromAssemblies(invokedOptions.GetActionsAssemblies())
                .AddClasses(classes => classes.AssignableTo<IAction>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return navigatorBuilder;
        }
    }
}