using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a game.
/// </summary>
[ActionType(nameof(GameAction))]
public abstract class GameAction : MessageAction
{
    /// <summary>
    /// Game information.
    /// </summary>
    public readonly Game Game;

    /// <inheritdoc />
    protected GameAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Game = Message.Game!;
    }
}