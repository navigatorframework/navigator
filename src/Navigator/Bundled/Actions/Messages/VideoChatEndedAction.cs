using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a video chat being ended.
/// </summary>
[ActionType(nameof(VideoChatEndedAction))]
public abstract class VideoChatEndedAction : MessageAction
{
    /// <summary>
    /// Information about the ended video chat.
    /// </summary>
    public readonly VideoChatEnded VideoChatEnded;

    /// <inheritdoc />
    protected VideoChatEndedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        VideoChatEnded = Message.VideoChatEnded!;
    }
}