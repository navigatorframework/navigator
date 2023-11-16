using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(MyChatMemberAction))]
public abstract class MyChatMemberAction : BaseAction
{
    /// <summary>
    /// Chat member updated.
    /// </summary>
    public ChatMemberUpdated ChatMemberUpdated { get; set; }
    
    /// <inheritdoc />
    protected MyChatMemberAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        ChatMemberUpdated = update.MyChatMember!;
    }
}