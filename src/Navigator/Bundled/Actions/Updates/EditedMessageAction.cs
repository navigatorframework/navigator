using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered when a message is edited.
/// <remarks>
///     Caution. Does not always works.
/// </remarks>
/// </summary>
[ActionType(nameof(EditedMessageAction))]
public abstract class EditedMessageAction : BaseAction
{
    /// <summary>
    /// Original <see cref="Message"/>.
    /// </summary>
    public Message OriginalMessage { get; protected set; }
        
    /// <summary>
    /// Edited <see cref="Message"/>.
    /// </summary>
    public Message EditedMessage { get; protected set; }

    /// <inheritdoc />
    protected EditedMessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        OriginalMessage = update.Message!;
        EditedMessage = update.EditedMessage!;
    }
}