using System.Collections.Generic;

namespace Navigator.Context.Extensions
{
    public static class NavigatorContextExtensions
    {
        public static TUpdate? GetOriginalUpdateOrDefault<TUpdate>(this INavigatorContext navigatorContext) where TUpdate : class
        {
            var update = navigatorContext.Extensions.GetValueOrDefault(OriginalUpdateContextExtension.OriginalUpdateKey);

            if (update is TUpdate originalUpdate)
            {
                return originalUpdate;
            }

            return default;
        }
    }
}