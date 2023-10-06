using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

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