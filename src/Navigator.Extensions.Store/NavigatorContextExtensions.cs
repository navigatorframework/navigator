using System;
using Navigator.Abstraction;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store
{
    public static class NavigatorContextExtensions
    {
        public static string DefaultUserKey => "navigator.extensions.store.user";
        public static string DefaultChatKey => "navigator.extensions.store.chat";

        #region User

        public static TUser GetUser<TUser>(this INavigatorContext ctx) where TUser : User
        {
            return ctx.GetUserOrDefault<TUser>() ?? throw new Exception("User not found");
        }
        
        public static TUser? GetUserOrDefault<TUser>(this INavigatorContext ctx, TUser defaultValue = default) where TUser : User
        {
            return ctx.Get<TUser>(DefaultUserKey) ?? defaultValue;
        }

        #endregion

        #region Chat

        public static TChat GetChat<TChat>(this INavigatorContext ctx) where TChat : Chat
        {
            return ctx.GetChatOrDefault<TChat>() ?? throw new Exception("User not found");
        }
        
        public static TChat? GetChatOrDefault<TChat>(this INavigatorContext ctx, TChat defaultValue = default) where TChat : Chat
        {
            return ctx.Get<TChat>(DefaultChatKey) ?? defaultValue;
        }

        #endregion
    }
}