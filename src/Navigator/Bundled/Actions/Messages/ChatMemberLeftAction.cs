using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a user when leaving a chat.
/// </summary>
[ActionType(nameof(ChatMemberLeftAction))]
public abstract class ChatMemberLeftAction : MessageAction
{
    /// <summary>
    /// Member who left the chat.
    /// </summary>
    public readonly User MemberLeft;

    /// <inheritdoc />
    protected ChatMemberLeftAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        MemberLeft = Message.LeftChatMember!;
    }
}