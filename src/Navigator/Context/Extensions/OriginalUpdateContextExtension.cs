using System.Threading.Tasks;

namespace Navigator.Context.Extensions
{
    public class OriginalUpdateContextExtension : INavigatorContextExtension
    {
        public const string OriginalUpdateKey = "_navigator.extensions.original_update";
        
        public Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
        {
            navigatorContext.Extensions.TryAdd(OriginalUpdateKey, builderOptions.GetOriginalUpdateOrDefault());

            return Task.FromResult(navigatorContext);
        }
    }
}