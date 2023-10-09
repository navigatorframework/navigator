using Navigator.Context;
using Navigator.Extensions.Bundled.OriginalEvent;
using Navigator.Extensions.Store.Context;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Store;

public static class NavigatorContextExtensions
{
    #region NavigatorStore
    
    /// <summary>
    /// Navigator store.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    /// <exception cref="NavigatorException"></exception>
    public static NavigatorDbContext Store(this INavigatorContext navigatorContext)
    {
        var store = navigatorContext.StoreOrDefault();
        
        return store ?? throw new NavigatorException($"Navigator store not found.");
    }
        
    /// <summary>
    /// Navigator store.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <returns></returns>
    public static NavigatorDbContext? StoreOrDefault(this INavigatorContext navigatorContext)
    {
        var store = navigatorContext.Extensions.GetValueOrDefault(StoreNavigatorContextExtension.StoreDbContext);

        if (store is NavigatorDbContext db)
        {
            return db;
        }

        return default;
    }

    #endregion
}