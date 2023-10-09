using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a chat photo being deleted.
/// </summary>
[ActionType(nameof(ChatPhotoDeletedAction))]
public abstract class ChatPhotoDeletedAction : MessageAction
{
    /// <inheritdoc />
    protected ChatPhotoDeletedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
}