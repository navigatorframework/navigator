using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by an audio file being sent.
/// /// </summary>
[ActionType(nameof(AudioAction))]
public abstract class AudioAction : MessageAction
{
    /// <summary>
    /// Audio information.
    /// </summary>
    public readonly Audio Audio;

    /// <inheritdoc />
    protected AudioAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Audio = Message.Audio!;
    }
}