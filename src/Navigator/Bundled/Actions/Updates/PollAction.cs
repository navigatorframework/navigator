using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

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
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent();

        Poll = update.Poll!;
    }
}