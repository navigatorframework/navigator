using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ChannelCreatedAction))]
public abstract class ChannelCreatedAction : BaseAction
{
    /// <inheritdoc />
    protected ChannelCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        
    }
}