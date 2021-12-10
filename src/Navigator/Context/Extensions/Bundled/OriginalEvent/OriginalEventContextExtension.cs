using System.Threading.Tasks;

namespace Navigator.Context.Extensions.Bundled.OriginalEvent
{
    internal class OriginalEventContextExtension : INavigatorContextExtension
    {
        public const string OriginalEventKey = "_navigator.extensions.original_event";
        
        public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
        {
            navigatorContext.Extensions.TryAdd(OriginalEventKey, builderOptions.GetOriginalEventOrDefault());

            return Task.FromResult(navigatorContext);
        }
    }
}