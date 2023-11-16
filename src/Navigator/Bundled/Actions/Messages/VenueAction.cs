using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a venue being sent.
/// </summary>
[ActionType(nameof(VenueAction))]
public abstract class VenueAction : MessageAction
{
    /// <summary>
    /// Venue information.
    /// </summary>
    public readonly Venue Venue;

    /// <inheritdoc />
    protected VenueAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Venue = Message.Venue!;
    }
}