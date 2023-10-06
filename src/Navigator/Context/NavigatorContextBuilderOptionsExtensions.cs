namespace Navigator.Context;

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

    private const string OriginalEventKey = "_navigator.context.options.original_event";

    public static void SetOriginalEvent(this INavigatorContextBuilderOptions contextBuilderOptions, object originalUpdate)
    {
        contextBuilderOptions.TryRegisterOption(OriginalEventKey, originalUpdate);

    }

    public static object? GetOriginalEventOrDefault(this INavigatorContextBuilderOptions contextBuilderOptions)
    {
        return contextBuilderOptions.RetrieveOption<object>(OriginalEventKey);
    }
        
    #endregion
}