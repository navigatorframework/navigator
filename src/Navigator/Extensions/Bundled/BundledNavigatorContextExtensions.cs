using Navigator.Context;
using Navigator.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Bundled;

/// <summary>
/// Bundled extensions to <see cref="NavigatorContext"/>.
/// </summary>
public static class BundledNavigatorContextExtensions
{
    /// <summary>
    /// Returns the <see cref="Update"/>. Throws an exception if not found.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    /// <exception cref="NavigatorException"></exception>
    public static Update GetUpdate(this INavigatorContext navigatorContext)
    {
        var update = navigatorContext.GetUpdateOrDefault();

        return update ?? throw new NavigatorException("Update was not found.");
    }

    /// <summary>
    /// Returns the <see cref="Update"/> or null if not found.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    public static Update? GetUpdateOrDefault(this INavigatorContext navigatorContext)
    {
        var @event = navigatorContext.Extensions.GetValueOrDefault(OriginalEventContextExtension.OriginalEventKey);

        if (@event is Update update)
        {
            return update;
        }

        return default;
    }
}