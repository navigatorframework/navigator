using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

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
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent();

        OriginalMessage = update.Message;
        EditedMessage = update.EditedMessage;
    }
}