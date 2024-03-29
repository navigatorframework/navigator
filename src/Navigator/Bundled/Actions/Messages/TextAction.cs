using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Telegram;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a text message.
/// </summary>
[ActionType(nameof(TextAction))]
public abstract class TextAction : MessageAction
{
    /// <summary>
    /// Message text.
    /// </summary>
    public readonly string Text;

    /// <inheritdoc />
    protected TextAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Text = Message.Text!;
    }
}