using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a supergroup being created.
/// </summary>
[ActionType(nameof(SupergroupCreatedAction))]
public abstract class SupergroupCreatedAction : BaseAction
{
    /// <inheritdoc />
    protected SupergroupCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        
    }

}