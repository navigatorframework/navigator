using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a video chat being started.
/// </summary>
[ActionType(nameof(VideoChatStartedAction))]
public abstract class VideoChatStartedAction : MessageAction
{
    /// <summary>
    /// Information about the started video chat.
    /// </summary>
    public readonly VideoChatStarted VideoChatStarted;

    /// <inheritdoc />
    protected VideoChatStartedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        VideoChatStarted = Message.VideoChatStarted!;
    }
}