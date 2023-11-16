using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
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