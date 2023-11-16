using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by the auto delete timer being changed in a chat.
/// </summary>
[ActionType(nameof(MessageAutoDeleteTimerChangedAction))]
public abstract class MessageAutoDeleteTimerChangedAction : MessageAction
{
    /// <summary>
    /// The new auto-delete timer duration for messages in the chat.
    /// </summary>
    public readonly MessageAutoDeleteTimerChanged MessageAutoDeleteTime;

    /// <inheritdoc />
    protected MessageAutoDeleteTimerChangedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        MessageAutoDeleteTime = Message.MessageAutoDeleteTimerChanged!;
    }
}