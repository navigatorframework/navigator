using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Catalog.Factory;

namespace Navigator.Configuration;

/// <summary>
///     Navigator extensions for <see cref="IApplicationBuilder" />
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Retrieves an instance of <see cref="BotActionCatalogFactory" /> in order to build the navigator bot.
    /// </summary>
    /// <param name="builder">An instance of <see cref="IApplicationBuilder" /></param>
    /// <returns>An instance of <see cref="BotActionCatalogFactory" /></returns>
    public static BotActionCatalogFactory GetBot(this IApplicationBuilder builder)
    {
        return builder.ApplicationServices.GetRequiredService<BotActionCatalogFactory>();
    }
}