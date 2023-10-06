namespace Navigator.Context.Builder.Options.Extensions;

public static class NavigatorContextBuilderOptionsExtensions
{
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