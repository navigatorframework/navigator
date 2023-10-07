using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace Navigator.Configuration;

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

        var telegramUpdate = await ParseTelegramUpdate(context.Request.Body);

        if (telegramUpdate is not null)
        {
            var navigatorMiddleware = context.RequestServices.GetRequiredService<TelegramMiddleware>();

            await navigatorMiddleware.Process(telegramUpdate);
        }
    }
        
    private static async Task<Update?> ParseTelegramUpdate(Stream stream)
    {
        try
        {
            var update = await JsonSerializer.DeserializeAsync<Update>(stream);

            return update?.Id == default ? default : update;
        }
        catch
        {
            return default;
        }
    }
}