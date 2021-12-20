using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Bundled;

public class StoreContextExtension: INavigatorContextExtension
{
    public const string StoreKey = "_navigator.extensions.navigator_store";
    
    private readonly NavigatorDbContext _navigatorDbContext;

    public StoreContextExtension(NavigatorDbContext navigatorDbContext)
    {
        _navigatorDbContext = navigatorDbContext;
    }

    public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        navigatorContext.Extensions.Add(StoreKey, _navigatorDbContext);
        
        return Task.FromResult(navigatorContext);
    }
}