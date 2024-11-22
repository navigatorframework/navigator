using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    ///     Maps the Telegram web hook endpoint. This endpoint is used to receive updates from Telegran.
    /// </summary>
    /// <param name="app">An instance of <see cref="WebApplication"/>.</param>
    public static void MapNavigator(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<NavigatorOptions>>().Value;

        app.MapPost(options.GetWebHookEndpointOrDefault(),
            async (Update update, INavigatorStrategy strategy, ILogger<WebApplication> logger) =>
            {
                try
                {
                    logger.LogDebug("Received update {UpdateId}", update.Id);
                    await strategy.Invoke(update);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Unhandled error while processing update {UpdateId}", update.Id);
                    logger.LogDebug("Update details: {@Update}", update);
                }
            });
    }
}