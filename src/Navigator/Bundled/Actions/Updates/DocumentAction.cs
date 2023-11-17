using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a document being sent.
/// </summary>
[ActionType(nameof(DocumentAction))]
public abstract class DocumentAction : BaseAction
{
    /// <summary>
    /// Document.
    /// </summary>
    public Document Document { get; set; }

    /// <inheritdoc />
    public DocumentAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = Context.GetUpdate();
        Document = update.Message!.Document!;
    }

}