using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Inline result based action.
/// </summary>
[ActionType(nameof(ChosenInlineResultAction))]
public abstract class ChosenInlineResultAction : BaseAction
{
    /// <summary>
    /// The original chosen inline result.
    /// </summary>
    public ChosenInlineResult ChosenInlineResult { get; protected set; }
        
    /// <summary>
    /// The chosen result id.
    /// </summary>
    public string ResultId { get; protected set; }
        
    /// <summary>
    /// The original query.
    /// </summary>
    public string Query { get; protected set; }

    /// <inheritdoc />
    protected ChosenInlineResultAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();

        ChosenInlineResult = update.ChosenInlineResult!;
        ResultId = update.ChosenInlineResult!.ResultId;
        Query = update.ChosenInlineResult.Query;
    }
}