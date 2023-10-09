using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a proximity alert being triggered.
/// </summary>
[ActionType(nameof(ProximityAlertTriggeredAction))]
public abstract class ProximityAlertTriggeredAction : MessageAction
{
    /// <summary>
    /// Information about the triggered proximity alert.
    /// </summary>
    public readonly ProximityAlertTriggered ProximityAlert;

    /// <inheritdoc />
    protected ProximityAlertTriggeredAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        ProximityAlert = Message.ProximityAlertTriggered!;
    }
}