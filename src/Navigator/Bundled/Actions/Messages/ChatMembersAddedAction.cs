using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by users being added to a chat.
/// </summary>
[ActionType(nameof(ChatMembersAddedAction))]
public abstract class ChatMembersAddedAction : MessageAction
{
    /// <summary>
    /// Array of users added to the chat.
    /// </summary>
    public readonly User[] MembersAdded;

    /// <inheritdoc />
    protected ChatMembersAddedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        MembersAdded = Message.NewChatMembers!;
    }
}