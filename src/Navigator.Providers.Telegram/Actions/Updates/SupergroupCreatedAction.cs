using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Providers.Telegram.Actions.Updates;

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