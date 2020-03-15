using System;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Extensions
{
    public static class NavigatorContextExtensions
    {
        public static string DefaultUserKey => "navigator.extensions.store.user";
        public static string DefaultChatKey => "navigator.extensions.store.chat";

        #region User

        public static TUser GetUser<TUser>(this NavigatorContext ctx) where TUser : User
        {
            return ctx.GetUserOrDefault<TUser>() ?? throw new Exception("User not found");
        }
        
        public static TUser? GetUserOrDefault<TUser>(this NavigatorContext ctx, TUser defaultValue = default) where TUser : User
        {
            return ctx.Get<TUser>(DefaultUserKey) ?? defaultValue;
        }

        #endregion

        #region Chat

        public static TChat GetChat<TChat>(this NavigatorContext ctx) where TChat : Chat
        {
            return ctx.GetChatOrDefault<TChat>() ?? throw new Exception("User not found");
        }
        
        public static TChat? GetChatOrDefault<TChat>(this NavigatorContext ctx, TChat defaultValue = default) where TChat : Chat
        {
            return ctx.Get<TChat>(DefaultChatKey) ?? defaultValue;
        }

        #endregion
    }
}