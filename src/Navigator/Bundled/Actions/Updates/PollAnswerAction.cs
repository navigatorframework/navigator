using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

[ActionType(nameof(PollAnswerAction))]
public abstract class PollAnswerAction : BaseAction
{
    /// <summary>
    /// The original Poll.
    /// </summary>
    public PollAnswer Answer { get; protected set; }

    /// <inheritdoc />
    protected PollAnswerAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetOriginalEvent();

        Answer = update.PollAnswer!;
    }
}