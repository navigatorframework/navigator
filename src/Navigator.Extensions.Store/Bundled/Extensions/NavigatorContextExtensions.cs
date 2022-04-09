using Navigator.Context;

namespace Navigator.Extensions.Store.Bundled.Extensions;

public static class NavigatorContextExtensions
{
    public static INavigatorStore GetStore<TEvent>(this INavigatorContext navigatorContext) where TEvent : class
    {
        var store = navigatorContext.GetStoreOrDefault<TEvent>();
        
        return store ?? throw new NavigatorException("Store was not found.");
    }
        
    public static INavigatorStore GetStoreOrDefault<TEvent>(this INavigatorContext navigatorContext) where TEvent : class
    {
        var @store = navigatorContext.Extensions.GetValueOrDefault(StoreContextExtension.NavigatorStoreKey);

        if (store is INavigatorStore navigatorStore)
        {
            return navigatorStore;
        }

        return default;
    }
}