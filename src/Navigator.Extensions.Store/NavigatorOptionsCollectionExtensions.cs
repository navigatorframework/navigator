using System;
using Navigator.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Store
{
    public static class NavigatorOptionsCollectionExtensions
    {
        #region UserMapper

        private const string UserTypeKey = "_navigator.extensions.store.options.user_type";

        public static void SetUserType<TUser>(this INavigatorOptions navigatorOptions) where TUser : User
        {
            navigatorOptions.TryRegisterOption(UserTypeKey, typeof(TUser));
        }

        public static Type? GetUserType(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<Type>(UserTypeKey);
        }

        #endregion
        
        #region ChatMapper

        private const string ChatTypeKey = "_navigator.extensions.store.options.chat_type";

        public static void SetChatType<TChat>(this INavigatorOptions navigatorOptions)
        {
            navigatorOptions.TryRegisterOption(ChatTypeKey, typeof(TChat));
        }

        public static Type? GetChatType(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<Type>(ChatTypeKey);
        }

        #endregion
        
        #region UserMapper

        private const string UserMapperKey = "_navigator.extensions.store.options.user_mapper";

        public static void SetUserMapper<TUserMapper>(this INavigatorOptions navigatorOptions)
        {
            navigatorOptions.ForceRegisterOption(UserMapperKey, typeof(TUserMapper));
        }

        public static Type? GetUserMapper(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<Type>(UserMapperKey);
        }

        #endregion
        
        #region ChatMapper

        private const string ChatMapperKey = "_navigator.extensions.store.options.chat_mapper";

        public static void SetChatMapper<TChatMapper>(this INavigatorOptions navigatorOptions)
        {
            navigatorOptions.ForceRegisterOption(ChatMapperKey, typeof(TChatMapper));
        }

        public static Type? GetChatMapper(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<Type>(ChatMapperKey);
        }

        #endregion
    }
}