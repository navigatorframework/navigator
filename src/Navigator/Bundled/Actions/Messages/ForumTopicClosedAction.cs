using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a forum topic being closed.
/// </summary>
[ActionType(nameof(ForumTopicClosedAction))]
public abstract class ForumTopicClosedAction : MessageAction
{
    /// <summary>
    /// Information about the closed forum topic.
    /// </summary>
    public readonly ForumTopicClosed ForumTopicClosed;

    /// <inheritdoc />
    protected ForumTopicClosedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        ForumTopicClosed = Message.ForumTopicClosed!;
    }
}