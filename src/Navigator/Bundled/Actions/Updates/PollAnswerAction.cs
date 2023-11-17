using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a poll being answered.
/// </summary>
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
        var update = Context.GetUpdate();

        Answer = update.PollAnswer!;
    }
}