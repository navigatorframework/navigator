using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Provider;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram
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

            if (context.Request.ContentType != "application/json")
            {
                return;
            }

            var telegramUpdate = await ParseTelegramUpdate(context.Request.Body);

            if (telegramUpdate is not null)
            {
                await using (var scope = context.RequestServices.CreateAsyncScope())
                {
                    var navigatorMiddleware = scope.ServiceProvider.GetRequiredService<TelegramMiddleware>();

                    await navigatorMiddleware.Process(telegramUpdate);
                }
            }
        }
        
        private static async Task<Update?> ParseTelegramUpdate(Stream stream)
        {
            try
            {
                var reader = new StreamReader(stream);
                var update = JsonConvert.DeserializeObject<Update>(await reader.ReadToEndAsync());

                return update.Id == default ? default : update;
            }
            catch
            {
                return default;
            }
        }
    }
}