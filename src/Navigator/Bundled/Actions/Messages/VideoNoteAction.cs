using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a video note being sent.
/// </summary>
[ActionType(nameof(VideoNoteAction))]
public abstract class VideoNoteAction : MessageAction
{
    /// <summary>
    /// Video note information.
    /// </summary>
    public readonly VideoNote VideoNote;

    /// <inheritdoc />
    protected VideoNoteAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        VideoNote = Message.VideoNote!; // Assuming VideoNote is a property of the Message class.
    }
}