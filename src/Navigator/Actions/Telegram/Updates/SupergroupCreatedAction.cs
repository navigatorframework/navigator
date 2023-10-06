using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(SupergroupCreatedAction))]
public abstract class SupergroupCreatedAction : BaseAction
{
    /// <inheritdoc />
    protected SupergroupCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

}