using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a chat being from a group.
/// </summary>
[ActionType(nameof(MigratedFromGroupAction))]
public abstract class MigratedFromGroupAction : MessageAction
{
    /// <summary>
    /// Information about the original group before migration.
    /// </summary>
    public readonly Chat MigratedChat;
        
    /// <summary>
    /// New Id for the chat.
    /// </summary>
    public readonly long MigrateToChatId;

    /// <inheritdoc />
    protected MigratedFromGroupAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        MigratedChat = Message.Chat;
        MigrateToChatId = (long)Message.MigrateToChatId!;
    }
}