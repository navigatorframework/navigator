using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a forum topic being reopened.
/// </summary>
[ActionType(nameof(ForumTopicReopenedAction))]
public abstract class ForumTopicReopenedAction : MessageAction
{
    /// <summary>
    /// Information about the reopened forum topic.
    /// </summary>
    public readonly ForumTopicReopened ForumTopicReopened;

    /// <inheritdoc />
    protected ForumTopicReopenedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        ForumTopicReopened = Message.ForumTopicReopened!;
    }
}