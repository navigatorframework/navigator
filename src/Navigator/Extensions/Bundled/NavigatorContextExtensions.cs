using Navigator.Context;
using Navigator.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Bundled;

public static class NavigatorContextExtensions
{
    #region OriginalEvent

    
    public static Update GetOriginalEvent(this INavigatorContext navigatorContext)
    {
        var update = navigatorContext.GetOriginalEventOrDefault();
        
        return update ?? throw new NavigatorException("Update was not found.");
    }
        
    public static Update? GetOriginalEventOrDefault(this INavigatorContext navigatorContext)
    {
        var @event = navigatorContext.Extensions.GetValueOrDefault(OriginalEventContextExtension.OriginalEventKey);

        if (@event is Update originalEvent)
        {
            return originalEvent;
        }

        return default;
    }

    #endregion
}