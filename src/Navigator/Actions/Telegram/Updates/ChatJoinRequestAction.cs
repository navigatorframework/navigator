using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Actions.Telegram.Updates;

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