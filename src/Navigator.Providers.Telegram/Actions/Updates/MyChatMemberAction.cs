using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(MyChatMemberAction))]
public abstract class MyChatMemberAction : BaseAction
{
    /// <inheritdoc />
    protected MyChatMemberAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

}