using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(PollUpdateAction))]
public abstract class PollUpdateAction : BaseAction
{
    /// <summary>
    /// The original Poll.
    /// </summary>
    public Poll Poll { get; protected set; }

    /// <inheritdoc />
    protected PollUpdateAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        Poll = update.Poll!;
    }
}