using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates;

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
        var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();
            
        InlineQuery = update.InlineQuery!;
        Query = update.InlineQuery!.Query;
        Offset = update.InlineQuery.Offset;
    }
}