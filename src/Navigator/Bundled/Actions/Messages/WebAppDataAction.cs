using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by web app data.
/// </summary>
[ActionType(nameof(WebAppDataAction))]
public abstract class WebAppDataAction : MessageAction
{
    /// <summary>
    /// Web app data information.
    /// </summary>
    public readonly WebAppData WebAppData;

    /// <inheritdoc />
    protected WebAppDataAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        WebAppData = Message.WebAppData!;
    }
}