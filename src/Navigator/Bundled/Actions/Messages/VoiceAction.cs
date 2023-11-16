using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a voice message being sent.
/// /// </summary>
[ActionType(nameof(VoiceAction))]
public abstract class VoiceAction : MessageAction
{
    /// <summary>
    /// Voice message information.
    /// </summary>
    public readonly Voice Voice;

    /// <inheritdoc />
    protected VoiceAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Voice = Message.Voice!;
    }
}