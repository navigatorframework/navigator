using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a poll
/// </summary>
[ActionType(nameof(PollMessageAction))]
public abstract class PollMessageAction : MessageAction
{
    /// <summary>
    /// Information about the poll.
    /// </summary>
    public readonly Poll Poll;

    /// <inheritdoc />
    protected PollMessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Poll = Message.Poll!;
    }
}