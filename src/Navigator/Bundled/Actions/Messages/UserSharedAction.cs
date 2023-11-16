using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered when a user shared content.
/// </summary>
[ActionType(nameof(UserSharedAction))]
public abstract class UserSharedAction : MessageAction
{
    /// <summary>
    /// Information about the user-shared content.
    /// </summary>
    public readonly UserShared UserShared;

    /// <inheritdoc />
    protected UserSharedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        UserShared = Message.UserShared!;
    }
}