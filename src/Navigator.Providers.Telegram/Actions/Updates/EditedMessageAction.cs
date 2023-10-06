using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(EditedMessageAction))]
public abstract class EditedMessageAction : BaseAction
{
    /// <summary>
    /// TODO
    /// </summary>
    public Message OriginalMessage { get; protected set; }
        
    /// <summary>
    /// TODO
    /// </summary>
    public Message EditedMessage { get; protected set; }

    /// <inheritdoc />
    protected EditedMessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        OriginalMessage = update.Message;
        EditedMessage = update.EditedMessage;
    }
}