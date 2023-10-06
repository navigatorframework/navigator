using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Actions.Telegram.Updates;

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
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        Answer = update.PollAnswer!;
    }
}