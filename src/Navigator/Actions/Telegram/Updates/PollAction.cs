using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Actions.Telegram.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(PollAction))]
public abstract class PollAction : BaseAction
{
    /// <summary>
    /// The original Poll.
    /// </summary>
    public Poll Poll { get; protected set; }

    /// <inheritdoc />
    protected PollAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        Poll = update.Poll!;
    }
}