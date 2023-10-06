using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

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
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        ChatMemberUpdated = update.MyChatMember!;
    }
}