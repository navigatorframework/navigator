using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by a chat being migrated to a supergroup.
    /// </summary>
    [ActionType(nameof(MigratedToSupergroupAction))]
    public abstract class MigratedToSupergroupAction : MessageAction
    {
        /// <summary>
        /// Information about the migrated supergroup.
        /// </summary>
        public readonly Chat MigratedChat;
        
        /// <summary>
        /// Old Id for the chat.
        /// </summary>
        public readonly long MigrateFromChatId;

        /// <inheritdoc />
        protected MigratedToSupergroupAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            MigratedChat = Message.Chat;
            MigrateFromChatId = (long)Message.MigrateFromChatId!;
        }
    }
}
