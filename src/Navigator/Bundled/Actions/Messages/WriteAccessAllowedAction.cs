using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered when write access is allowed.
/// </summary>
[ActionType(nameof(WriteAccessAllowedAction))]
public abstract class WriteAccessAllowedAction : MessageAction
{
    /// <summary>
    /// Information about the allowed write access.
    /// </summary>
    public readonly WriteAccessAllowed WriteAccessAllowed;

    /// <inheritdoc />
    protected WriteAccessAllowedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        WriteAccessAllowed = Message.WriteAccessAllowed!;
    }
}