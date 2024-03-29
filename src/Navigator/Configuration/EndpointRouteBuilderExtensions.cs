using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Navigator.Configuration;

/// <summary>
/// Navigator extensions for <see cref="IEndpointRouteBuilder"/>.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Configure navigator's provider's endpoints.
    /// </summary>
    /// <param name="endpointRouteBuilder"></param>
    /// <returns></returns>
    public static void MapNavigator(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        using var scope = endpointRouteBuilder.ServiceProvider.CreateScope();

        var options = scope.ServiceProvider.GetRequiredService<NavigatorOptions>();

        endpointRouteBuilder.MapPost(options.GetWebHookEndpointOrDefault(), ProcessTelegramUpdate);
    }
        
    private static async Task ProcessTelegramUpdate(HttpContext context)
    {
        context.Response.StatusCode = 200;

        if (context.Request.ContentType != "application/json")
        {
            return;
        }

        var telegramUpdate = await ParseTelegramUpdate(context.Request);

        var navigatorMiddleware = context.RequestServices.GetRequiredService<TelegramMiddleware>();

        await navigatorMiddleware.Process(telegramUpdate);
    }
        
    private static async Task<Update> ParseTelegramUpdate(HttpRequest request)
    {
        try
        {
            var reader = new StreamReader(request.Body);
            return JsonConvert.DeserializeObject<Update>(await reader.ReadToEndAsync()) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            throw new NavigatorException("Cannot parse telegram update", e);
        }
    }
}