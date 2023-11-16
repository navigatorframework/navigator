using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// TODO
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