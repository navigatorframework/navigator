using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Navigator.Abstractions;

namespace Navigator
{
    public static class EndpointRouteBuilderExtensions
    {
        public static NavigatorRouteConfiguration MapNavigator(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            using var scope = endpointRouteBuilder.ServiceProvider.CreateScope();

            var options = scope.ServiceProvider.GetRequiredService<NavigatorOptions>();

            endpointRouteBuilder.MapPost(options.GetWebHookEndpointOrDefault(), ProcessTelegramUpdate);

            return new NavigatorRouteConfiguration(endpointRouteBuilder);
        }
        
        public static 

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