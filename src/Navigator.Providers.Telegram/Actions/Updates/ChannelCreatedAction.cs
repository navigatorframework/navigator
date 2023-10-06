using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

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