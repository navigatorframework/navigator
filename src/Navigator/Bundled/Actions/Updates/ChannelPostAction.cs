using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

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
        var update = Context.GetOriginalEvent();

        ChannelPost = update.ChannelPost!;
    }
}