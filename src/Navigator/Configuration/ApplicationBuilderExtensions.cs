using Microsoft.AspNetCore.Builder;
using Navigator.Catalog;

namespace Navigator.Configuration;

/// <summary>
/// Navigator extensions for <see cref="IApplicationBuilder"/>
/// </summary>
public static class ApplicationBuilderExtensions
{
    public static IBotActionCatalogBuilder GetBot(this IApplicationBuilder builder)
    {
        return new BotActionCatalogBuilder();
    }

}