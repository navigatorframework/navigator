using Navigator.Context;
using Navigator.Context.Builder.Options;
using Navigator.Extensions;

namespace Navigator.Bundled.Extensions.Update;

internal class UpdateNavigatorContextExtension : INavigatorContextExtension
{
    public const string UpdateKey = "_navigator.extensions.update";
        
    public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        navigatorContext.Extensions.TryAdd(UpdateKey, builderOptions.GetUpdateOrDefault());

        return Task.FromResult(navigatorContext);
    }
}