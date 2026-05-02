using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Entities;
using Navigator.Abstractions.Strategies;
using Navigator.Configuration.Options;
using Navigator.Strategy;
using Telegram.Bot.Types;

namespace Navigator.Configuration;

/// <summary>
///     Navigator extensions for <see cref="IEndpointRouteBuilder" />.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    ///     Maps the Telegram web hook endpoint. This endpoint is used to receive updates from Telegram.
    /// </summary>
    /// <param name="app">An instance of <see cref="WebApplication"/>.</param>
    public static void MapNavigator(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<NavigatorOptions>>().Value;

        app.MapPost(options.GetWebHookEndpointOrDefault(),
            async (Update update, INavigatorStrategy strategy, HttpContext httpContext, ILogger<NavigatorEndpointReceiver> logger) =>
            {
                var identifier = httpContext.TraceIdentifier;
                try
                {
                    logger.LogDebug("Received update {UpdateId} with identifier {Identifier}", update.Id, identifier);
                    await strategy.Invoke(update, identifier);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Unhandled error while processing update {UpdateId} with {Identifier}", 
                        update.Id, identifier);
                    logger.LogDebug("Update details: {@Update}", update);
                }
            });
    }
}

/// <summary>
///     Marker type used for webhook endpoint logging.
/// </summary>
public class NavigatorEndpointReceiver;
