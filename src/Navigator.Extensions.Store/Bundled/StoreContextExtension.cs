using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Bundled;

public class StoreContextExtension: INavigatorContextExtension
{
    public const string StoreKey = "_navigator.extensions.store.navigator_db_context";
    
    private readonly IUniversalStore _universalStore;

    public StoreContextExtension(IUniversalStore universalStore)
    {
        _universalStore = universalStore;
    }

    public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        navigatorContext.Extensions.Add(StoreKey, _universalStore);
        
        return Task.FromResult(navigatorContext);
    }
}