using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered when a chat member has been updated.
/// </summary>
[ActionType(nameof(ChatMemberAction))]
public abstract class ChatMemberAction : BaseAction
{
    /// <summary>
    /// Chat member updated.
    /// </summary>
    public ChatMemberUpdated ChatMemberUpdated { get; set; }

    /// <inheritdoc />
    protected ChatMemberAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        ChatMemberUpdated = update.ChatMember!;
    }
}