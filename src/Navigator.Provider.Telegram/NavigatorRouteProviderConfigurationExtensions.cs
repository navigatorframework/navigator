using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions;
using Navigator.Configuration;
using Navigator.Configuration.Provider;

namespace Navigator.Provider.Telegram
{
    public static class NavigatorRouteProviderConfigurationExtensions
    {
        public static NavigatorRouteConfiguration Telegram(this NavigatorRouteProviderConfiguration routeProviderConfiguration)
        {
            return routeProviderConfiguration.Provider(builder =>
            {
                using var scope = builder.ServiceProvider.CreateScope();

                var options = scope.ServiceProvider.GetRequiredService<NavigatorOptions>();

                builder.MapPost(options.GetWebHookEndpointOrDefault(), ProcessTelegramUpdate);
            });
        }
        
        private static async Task ProcessTelegramUpdate(HttpContext context)
        {
            context.Response.StatusCode = 200;

            if (context.Request.ContentType == "application/json")
            {
                var navigatorMiddleware = context.RequestServices.GetRequiredService<INavigatorMiddleware>();

                await navigatorMiddleware.Handle(context.Request);
            }
        }
    }
}