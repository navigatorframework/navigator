using Navigator.Context;
using Navigator.Context.Builder.Options;
using Navigator.Context.Builder.Options.Extensions;

namespace Navigator.Extensions.Bundled.OriginalEvent;

internal class OriginalEventContextExtension : INavigatorContextExtension
{
    public const string OriginalEventKey = "_navigator.extensions.original_event";
        
    public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        navigatorContext.Extensions.TryAdd(OriginalEventKey, builderOptions.GetOriginalEventOrDefault());

        return Task.FromResult(navigatorContext);
    }
}