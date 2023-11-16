using Navigator.Context.Builder.Options;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Extensions.ActionType;

internal static class NavigatorContextBuilderOptionsExtensions
{
    #region ActionType

    private const string ActionTypeKey = "_navigator.context.options.action_type";

    public static void SetActionType(this INavigatorContextBuilderOptions contextBuilderOptions, string actionType)
    {
        contextBuilderOptions.TryRegisterOption(ActionTypeKey, actionType);

    }

    public static string? GetActionType(this INavigatorContextBuilderOptions contextBuilderOptions)
    {
        return contextBuilderOptions.RetrieveOption<string>(ActionTypeKey);
    }
        
    #endregion
}