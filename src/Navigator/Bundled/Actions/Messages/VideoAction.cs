using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by an video file being sent.
/// /// </summary>
[ActionType(nameof(VideoAction))]
public abstract class VideoAction : MessageAction
{
    /// <summary>
    /// Video information.
    /// </summary>
    public readonly Video Audio;

    /// <inheritdoc />
    protected VideoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Audio = Message.Video!;
    }
}