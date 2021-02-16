using System;

namespace Navigator.Context
{
    public static class NavigatorContextBuilderOptionsExtensions
    {
        #region Provider

        private const string ProviderKey = "_navigator.context.options.provider";

        public static void Provider<TProvider>(this NavigatorContextBuilderOptions contextBuilderOptions) where TProvider : IProvider, new()
        {
            contextBuilderOptions.TryRegisterOption(ProviderKey, typeof(TProvider));

        }

        public static Type? GetProvider(this INavigatorContextBuilderOptions contextBuilderOptions)
        {
            return contextBuilderOptions.RetrieveOption<Type>(ProviderKey);
        }
        
        #endregion
    }
}