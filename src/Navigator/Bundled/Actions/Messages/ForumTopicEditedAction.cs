using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a forum topic being edited.
/// </summary>
[ActionType(nameof(ForumTopicEditedAction))]
public abstract class ForumTopicEditedAction : MessageAction
{
    /// <summary>
    /// Information about the edited forum topic.
    /// </summary>
    public readonly ForumTopicEdited ForumTopicEdited;

    /// <inheritdoc />
    protected ForumTopicEditedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        ForumTopicEdited = Message.ForumTopicEdited!;
    }
}