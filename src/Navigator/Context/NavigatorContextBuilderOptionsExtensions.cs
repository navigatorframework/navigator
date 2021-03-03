using System;
using Navigator.Entities;

namespace Navigator.Context
{
    public static class NavigatorContextBuilderOptionsExtensions
    {
        #region Provider

        private const string ProviderKey = "_navigator.context.options.provider";

        public static void SetProvider<TProvider>(this INavigatorContextBuilderOptions contextBuilderOptions) where TProvider : INavigatorProvider
        {
            contextBuilderOptions.TryRegisterOption(ProviderKey, typeof(TProvider));

        }

        public static Type? GetProvider(this INavigatorContextBuilderOptions contextBuilderOptions)
        {
            return contextBuilderOptions.RetrieveOption<Type>(ProviderKey);
        }
        
        #endregion
        
        #region ActionType

        private const string ActionTypeKey = "_navigator.context.options.action_type";

        public static void SetActionType(this INavigatorContextBuilderOptions contextBuilderOptions, string actionType)
        {
            contextBuilderOptions.TryRegisterOption(ActionTypeKey, actionType);

        }

        public static string? GetAcitonType(this INavigatorContextBuilderOptions contextBuilderOptions)
        {
            return contextBuilderOptions.RetrieveOption<string>(ActionTypeKey);
        }
        
        #endregion
        
        #region OriginalUpdate

        private const string OriginalUpdateKey = "_navigator.context.options.original_update";

        public static void SetOriginalUpdate(this INavigatorContextBuilderOptions contextBuilderOptions, object originalUpdate)
        {
            contextBuilderOptions.TryRegisterOption(OriginalUpdateKey, originalUpdate);

        }

        public static object? GetOriginalUpdateOrDefault(this INavigatorContextBuilderOptions contextBuilderOptions)
        {
            return contextBuilderOptions.RetrieveOption<object>(OriginalUpdateKey);
        }
        
        #endregion
        
        #region Conversation

        private const string ConversationKey = "_navigator.context.options.conversation";

        public static void SetConversation(this INavigatorContextBuilderOptions contextBuilderOptions, IConversation conversation)
        {
            contextBuilderOptions.TryRegisterOption(ConversationKey, conversation);

        }

        public static IConversation? GetConversationOrDefault(this INavigatorContextBuilderOptions contextBuilderOptions)
        {
            return contextBuilderOptions.RetrieveOption<IConversation>(ConversationKey);
        }
        
        #endregion
    }
}