using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a group being created.
/// </summary>
[ActionType(nameof(GroupCreatedAction))]
public abstract class GroupCreatedAction : MessageAction
{
    /// <summary>
    /// Information about the created group.
    /// </summary>
    public readonly Chat CreatedGroup;

    /// <inheritdoc />
    protected GroupCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        CreatedGroup = Message.Chat;
    }
}