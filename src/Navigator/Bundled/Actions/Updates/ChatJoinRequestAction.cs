using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
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
        var update = Context.GetUpdate();

        Request = update.ChatJoinRequest!;
    }
}