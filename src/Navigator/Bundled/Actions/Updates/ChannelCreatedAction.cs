using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a Channel being created.
/// </summary>
[ActionType(nameof(ChannelCreatedAction))]
public abstract class ChannelCreatedAction : BaseAction
{
    /// <inheritdoc />
    protected ChannelCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        
    }
}