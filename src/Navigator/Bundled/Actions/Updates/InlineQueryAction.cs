using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Inline query based action.
/// </summary>
[ActionType(nameof(InlineQueryAction))]
public abstract class InlineQueryAction : BaseAction
{
    /// <summary>
    /// The original <see cref="Update.InlineQuery"/>
    /// </summary>
    public InlineQuery InlineQuery { get; protected set; }
        
    /// <summary>
    /// The query from the user.
    /// </summary>
    public string Query { get; protected set; }
        
    /// <summary>
    /// The offset.
    /// </summary>
    public string Offset { get; protected set; }

    /// <inheritdoc />
    protected InlineQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();
            
        InlineQuery = update.InlineQuery!;
        Query = update.InlineQuery!.Query;
        Offset = update.InlineQuery.Offset;
    }
}