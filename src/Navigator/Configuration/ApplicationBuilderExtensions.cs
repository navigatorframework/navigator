using Microsoft.AspNetCore.Builder;
using Navigator.Catalog;

namespace Navigator.Configuration;

/// <summary>
/// Navigator extensions for <see cref="IApplicationBuilder"/>
/// </summary>
public static class ApplicationBuilderExtensions
{
    public static IBotActionCatalogFactory GetBot(this IApplicationBuilder builder)
    {
        return new BotActionCatalogFactory();
    }

}