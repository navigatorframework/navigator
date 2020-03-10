using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Navigator.Configuration;
using Navigator.Middleware;

namespace Navigator
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapNavigator(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            using var scope = endpointRouteBuilder.ServiceProvider.CreateScope();

            var options = scope.ServiceProvider.GetRequiredService<IOptions<NavigatorOptions>>();

            endpointRouteBuilder.MapPost(options.Value.EndpointWebHook, ProcessTelegramUpdate);
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