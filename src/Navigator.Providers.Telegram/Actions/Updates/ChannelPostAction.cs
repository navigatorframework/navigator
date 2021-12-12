using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ChannelPostAction))]
public abstract class ChannelPostAction : BaseAction
{
    /// <inheritdoc />
    protected ChannelPostAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

        

}