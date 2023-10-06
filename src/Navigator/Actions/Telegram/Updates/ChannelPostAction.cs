using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Actions.Telegram.Updates;

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