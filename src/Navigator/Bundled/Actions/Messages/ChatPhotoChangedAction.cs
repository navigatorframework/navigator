using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a chat photo changing.
/// </summary>
[ActionType(nameof(ChatPhotoChangedAction))]
public abstract class ChatPhotoChangedAction : MessageAction
{
    /// <summary>
    /// Array of photo sizes representing the new chat photo.
    /// </summary>
    public readonly PhotoSize[] NewChatPhoto;

    /// <inheritdoc />
    protected ChatPhotoChangedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        NewChatPhoto = Message.NewChatPhoto!;
    }
}