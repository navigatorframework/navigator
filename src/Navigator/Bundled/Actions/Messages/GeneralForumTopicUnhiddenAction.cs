using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a general forum topic being unhidden.
/// </summary>
[ActionType(nameof(GeneralForumTopicUnhiddenAction))]
public abstract class GeneralForumTopicUnhiddenAction : MessageAction
{
    /// <summary>
    /// Information about the unhidden general forum topic.
    /// </summary>
    public readonly GeneralForumTopicUnhidden GeneralForumTopicUnhidden;

    /// <inheritdoc />
    protected GeneralForumTopicUnhiddenAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        GeneralForumTopicUnhidden = Message.GeneralForumTopicUnhidden!;
    }
}