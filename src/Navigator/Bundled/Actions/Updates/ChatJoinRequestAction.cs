using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

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
        var update = Context.GetOriginalEvent();

        Request = update.ChatJoinRequest!;
    }
}