using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered when chat shared content.
    /// </summary>
    [ActionType(nameof(ChatSharedAction))]
    public abstract class ChatSharedAction : MessageAction
    {
        /// <summary>
        /// Information about the chat-shared content.
        /// </summary>
        public readonly ChatShared ChatShared;

        /// <inheritdoc />
        protected ChatSharedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            ChatShared = Message.ChatShared!;
        }
    }
}
