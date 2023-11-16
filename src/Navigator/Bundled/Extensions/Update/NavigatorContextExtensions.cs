using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Extensions.Update;

/// <summary>
/// Bundled extensions to <see cref="NavigatorContext"/>.
/// </summary>
public static class NavigatorContextExtensions
{
    /// <summary>
    /// Returns the <see cref="Update"/>. Throws an exception if not found.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    /// <exception cref="NavigatorException"></exception>
    public static global::Telegram.Bot.Types.Update GetUpdate(this INavigatorContext navigatorContext)
    {
        var update = navigatorContext.GetUpdateOrDefault();

        return update ?? throw new NavigatorException("Update was not found.");
    }

    /// <summary>
    /// Returns the <see cref="Update"/> or null if not found.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    public static global::Telegram.Bot.Types.Update? GetUpdateOrDefault(this INavigatorContext navigatorContext)
    {
        var @event = navigatorContext.Extensions.GetValueOrDefault(UpdateNavigatorContextExtension.UpdateKey);

        if (@event is global::Telegram.Bot.Types.Update update)
        {
            return update;
        }

        return default;
    }
}