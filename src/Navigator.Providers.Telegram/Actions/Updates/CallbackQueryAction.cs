using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// A callback query based action.
/// </summary>
[ActionType(nameof(CallbackQueryAction))]
public abstract class CallbackQueryAction : BaseAction
{
    /// <summary>
    /// The original <see cref="Update.CallbackQuery"/>
    /// </summary>
    public CallbackQuery CallbackQuery { get; protected set; }

    /// <summary>
    /// The message that originated the callback query. Iy may be null if the message is too old.
    /// </summary>
    public Message? OriginalMessage { get; protected set; }

    /// <summary>
    /// Any data present on the callback query.
    /// </summary>
    public string? Data { get; protected set; }

    /// <summary>
    /// True if the callback query is from a game.
    /// </summary>
    public bool IsGameQuery { get; protected set; }

    /// <inheritdoc />
    protected CallbackQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        CallbackQuery = update.CallbackQuery!;
        OriginalMessage = CallbackQuery.Message;
        Data = string.IsNullOrWhiteSpace(CallbackQuery.Data) ? CallbackQuery.Data : default;
        IsGameQuery = CallbackQuery.IsGameQuery;
    }
}