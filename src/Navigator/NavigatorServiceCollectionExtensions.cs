using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Hosted;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static IServiceCollection AddNavigator(this IServiceCollection services, Action<NavigatorOptions> navigatorOptions)
        {
            if (navigatorOptions == null)
            {
                throw new ArgumentNullException(nameof(navigatorOptions), "Navigator options are required for navigator framework to work.");
            }
            services.Configure(navigatorOptions);

            services.AddHostedService<SetTelegramBotWebHookHostedService>();
            
            return services;
        }
    }
}