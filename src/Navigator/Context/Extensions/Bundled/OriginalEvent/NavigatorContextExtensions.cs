using System.Collections.Generic;

namespace Navigator.Context.Extensions.Bundled.OriginalEvent;

public static class NavigatorContextExtensions
{
    #region OriginalEvent

    public static TEvent GetOriginalEvent<TEvent>(this INavigatorContext navigatorContext) where TEvent : class
    {
        var update = navigatorContext.GetOriginalEventOrDefault<TEvent>();
        
        return update ?? throw new NavigatorException("Update was not found.");
    }
        
    public static TEvent? GetOriginalEventOrDefault<TEvent>(this INavigatorContext navigatorContext) where TEvent : class
    {
        var @event = navigatorContext.Extensions.GetValueOrDefault(OriginalEventContextExtension.OriginalEventKey);

        if (@event is TEvent originalEvent)
        {
            return originalEvent;
        }

        return default;
    }

    #endregion
}