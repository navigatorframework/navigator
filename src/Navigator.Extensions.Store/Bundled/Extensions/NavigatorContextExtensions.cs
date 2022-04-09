using Navigator.Context;

namespace Navigator.Extensions.Store.Bundled.Extensions;

public static class NavigatorContextExtensions
{
    public static INavigatorStore GetStore(this INavigatorContext navigatorContext)
    {
        var store = navigatorContext.GetStoreOrDefault();
        
        return store ?? throw new NavigatorException("Store was not found.");
    }
        
    public static INavigatorStore? GetStoreOrDefault(this INavigatorContext navigatorContext)
    {
        var @store = navigatorContext.Extensions.GetValueOrDefault(StoreContextExtension.NavigatorStoreKey);

        if (store is INavigatorStore navigatorStore)
        {
            return navigatorStore;
        }

        return default;
    }
}