using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a website being connected.
/// </summary>
[ActionType(nameof(WebsiteConnectedAction))]
public abstract class WebsiteConnectedAction : MessageAction
{
    /// <summary>
    /// Entity representing the website connected.
    /// </summary>
    public readonly string WebsiteConnected;

    /// <inheritdoc />
    protected WebsiteConnectedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        WebsiteConnected = Message.ConnectedWebsite!;
    }
}