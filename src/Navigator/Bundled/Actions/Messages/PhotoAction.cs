using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a photo being sent.
/// /// </summary>
[ActionType(nameof(PhotoAction))]
public abstract class PhotoAction : MessageAction
{
    /// <summary>
    /// Photo information.
    /// </summary>
    public readonly PhotoSize[] Photo;

    /// <inheritdoc />
    protected PhotoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Photo = Message.Photo!;
    }
}