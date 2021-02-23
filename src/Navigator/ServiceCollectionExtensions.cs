using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Configuration;
using Navigator.Context;
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

            services.AddNavigatorContextServices();
            
            services.AddScoped<IActionLauncher, ActionLauncher>();
            
            services.Scan(scan => scan
                .FromAssemblies(navigatorBuilder.Options.GetActionsAssemblies())
                .AddClasses(classes => classes.AssignableTo<IAction>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            navigatorBuilder.RegisterOrReplaceOptions();
            
            return navigatorBuilder;
        }

        internal static void AddNavigatorContextServices(this IServiceCollection services)
        {
            services.AddScoped<INavigatorContextBuilder, NavigatorContextBuilder>();
            services.AddScoped<INavigatorContextFactory, NavigatorContextFactory>();
            services.AddScoped<INavigatorContextAccessor, NavigatorContextAccessor>();
        }
    }
}