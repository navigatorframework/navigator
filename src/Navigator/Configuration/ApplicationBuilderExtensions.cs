using Microsoft.AspNetCore.Builder;
using Action = Navigator.Actions.Action;

namespace Navigator.Configuration;

/// <summary>
/// Navigator extensions for <see cref="IApplicationBuilder"/>
/// </summary>
public static class ApplicationBuilderExtensions
{
    public static INavigatorBotBuilder GetBot(this IApplicationBuilder builder)
    {
        return new NavigatorBotBuilder();
    }

}