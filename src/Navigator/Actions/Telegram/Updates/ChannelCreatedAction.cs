using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram.Updates;

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