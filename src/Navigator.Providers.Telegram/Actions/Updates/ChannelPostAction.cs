using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ChannelPostAction))]
public abstract class ChannelPostAction : BaseAction
{
    /// <summary>
    /// Channel post message.
    /// </summary>
    public Message ChannelPost { get; protected set; }
    
    /// <inheritdoc />
    protected ChannelPostAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        ChannelPost = update.ChannelPost!;
    }
}