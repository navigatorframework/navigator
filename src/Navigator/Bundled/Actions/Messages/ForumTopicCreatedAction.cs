using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by a forum topic being created.
    /// </summary>
    [ActionType(nameof(ForumTopicCreatedAction))]
    public abstract class ForumTopicCreatedAction : MessageAction
    {
        /// <summary>
        /// Information about the created forum topic.
        /// </summary>
        public readonly ForumTopicCreated ForumTopicCreated;

        /// <inheritdoc />
        protected ForumTopicCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            ForumTopicCreated = Message.ForumTopicCreated!;
        }
    }
}
