using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a general forum topic being hidden.
/// </summary>
[ActionType(nameof(GeneralForumTopicHiddenAction))]
public abstract class GeneralForumTopicHiddenAction : MessageAction
{
    /// <summary>
    /// Information about the hidden general forum topic.
    /// </summary>
    public readonly GeneralForumTopicHidden GeneralForumTopicHidden;

    /// <inheritdoc />
    protected GeneralForumTopicHiddenAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        GeneralForumTopicHidden = Message.GeneralForumTopicHidden!;
    }
}