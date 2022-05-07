using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ChatJoinRequestAction))]
public abstract class ChatJoinRequestAction : BaseAction
{
    /// <summary>
    /// Chat join request.
    /// </summary>
    public ChatJoinRequest Request { get; protected set; }

    /// <inheritdoc />
    protected ChatJoinRequestAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        Request = update.ChatJoinRequest!;
    }
}