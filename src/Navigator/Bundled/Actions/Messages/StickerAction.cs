using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a sticker being sent.
/// /// </summary>
[ActionType(nameof(StickerAction))]
public abstract class StickerAction : MessageAction
{
    /// <summary>
    /// Sticker.
    /// </summary>
    public readonly Sticker Sticker;

    /// <inheritdoc />
    protected StickerAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Sticker = Message.Sticker!;
    }
}