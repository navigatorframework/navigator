using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action being triggered by a chat title changing.
/// </summary>
[ActionType(nameof(ChatTitleChangedAction))]
public abstract class ChatTitleChangedAction : MessageAction
{
    /// <summary>
    /// The new title of the chat.
    /// </summary>
    public readonly string NewChatTitle;

    /// <inheritdoc />
    protected ChatTitleChangedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        NewChatTitle = Message.NewChatTitle!;
    }
}