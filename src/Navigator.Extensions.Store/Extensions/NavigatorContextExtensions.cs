using Navigator.Extensions.Store.Provider;

namespace Navigator.Extensions.Store.Extensions
{
    public static class NavigatorContextExtensions
    {
        public static string DefaultUserKey => "navigator.extensions.store.user";

        
        
        public static TUser GetUserOrDefault<TUser>(this NavigatorContext ctx, TUser tUserDefault = default)
        {
            return ctx.Get<TUser>(DefaultUserKey) ?? tUserDefault;
        }
    }
}