using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// A callback query based action.
/// </summary>
[ActionType(nameof(CallbackQueryAction))]
public abstract class CallbackQueryAction : BaseAction
{
    /// <summary>
    /// The original <see cref="Update.CallbackQuery"/>
    /// </summary>
    public readonly CallbackQuery CallbackQuery;

    /// <summary>
    /// The message that originated the callback query. Iy may be unavailable if the message is too old.
    /// </summary>
    public readonly Message? OriginalMessage;

    /// <summary>
    /// Any data present on the callback query.
    /// </summary>
    public readonly string? Data;

    /// <summary>
    /// True if the callback query is from a game.
    /// </summary>
    public readonly bool IsGameQuery;

    /// <inheritdoc />
    protected CallbackQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        CallbackQuery = update.CallbackQuery!;
        OriginalMessage = CallbackQuery.Message;
        Data = string.IsNullOrWhiteSpace(CallbackQuery.Data) ? CallbackQuery.Data : default;
        IsGameQuery = CallbackQuery.IsGameQuery;
    }
}