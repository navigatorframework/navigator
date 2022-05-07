using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ChatJoinRequestAction))]
public abstract class ChatJoinRequestAction : BaseAction
{
    /// <inheritdoc />
    protected ChatJoinRequestAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

}