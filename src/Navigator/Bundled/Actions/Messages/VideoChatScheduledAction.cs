using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a video chat being scheduled.
/// </summary>
[ActionType(nameof(VideoChatScheduledAction))]
public abstract class VideoChatScheduledAction : MessageAction
{
    /// <summary>
    /// Information about the scheduled video chat.
    /// </summary>
    public readonly VideoChatScheduled VideoChatScheduled;

    /// <inheritdoc />
    protected VideoChatScheduledAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        VideoChatScheduled = Message.VideoChatScheduled!;
    }
}