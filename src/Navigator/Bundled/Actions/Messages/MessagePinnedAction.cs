using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a message being pinned.
/// </summary>
[ActionType(nameof(MessagePinnedAction))]
public abstract class MessagePinnedAction : MessageAction
{
    /// <summary>
    /// ID of the pinned message.
    /// </summary>
    public readonly Message PinnedMessageId;

    /// <inheritdoc />
    protected MessagePinnedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        PinnedMessageId = Message.PinnedMessage!;
    }
}