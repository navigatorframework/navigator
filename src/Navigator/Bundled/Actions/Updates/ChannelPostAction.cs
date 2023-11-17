using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a post being sent to a channel.
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
        var update = Context.GetUpdate();

        ChannelPost = update.ChannelPost!;
    }
}