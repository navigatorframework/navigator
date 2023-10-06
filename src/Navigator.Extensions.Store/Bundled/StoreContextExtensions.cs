using Navigator.Context;
using Navigator.Context.Extensions;

namespace Navigator.Extensions.Store.Bundled;

internal class StoreContextExtension : INavigatorContextExtension
{
    public const string NavigatorStoreKey = "_navigator.extensions.store.navigator_store";

    private readonly INavigatorStore _navigatorStore;

    public StoreContextExtension(INavigatorStore navigatorStore)
    {
        _navigatorStore = navigatorStore;
    }

    public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        navigatorContext.Extensions.TryAdd(NavigatorStoreKey, _navigatorStore);

        return Task.FromResult(navigatorContext);
    }
}