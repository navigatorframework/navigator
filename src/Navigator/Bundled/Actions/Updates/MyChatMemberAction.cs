using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered when the bot’s chat member status was updated in a chat.
/// For private chats, this update is received only when the bot is blocked or unblocked by the user.
/// </summary>
[ActionType(nameof(MyChatMemberAction))]
public abstract class MyChatMemberAction : BaseAction
{
    /// <summary>
    /// Chat member updated.
    /// </summary>
    public ChatMemberUpdated MyChatMember { get; set; }
    
    /// <inheritdoc />
    protected MyChatMemberAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        MyChatMember = update.MyChatMember!;
    }
}