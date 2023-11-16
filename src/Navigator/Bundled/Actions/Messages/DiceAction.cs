using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by a Dice.
/// </summary>
[ActionType(nameof(DiceAction))]
public abstract class DiceAction : MessageAction
{
    /// <summary>
    /// Information about the dice.
    /// </summary>
    public readonly Dice Dice;

    /// <inheritdoc />
    protected DiceAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Dice = Message.Dice!;
    }
}