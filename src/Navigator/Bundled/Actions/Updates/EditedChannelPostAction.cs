using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a post sent to a channel.
/// </summary>
[ActionType(nameof(EditedChannelPostAction))]
public abstract class EditedChannelPostAction : BaseAction
{
    /// <summary>
    /// Edited channel post.
    /// </summary>
    public Message ChannelPost { get; set; }

    /// <inheritdoc />
    protected EditedChannelPostAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
       ChannelPost = Context.GetUpdate().EditedChannelPost!;
    }

}