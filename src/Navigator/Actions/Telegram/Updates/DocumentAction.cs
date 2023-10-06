using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(DocumentAction))]
public abstract class DocumentAction : BaseAction
{
    /// <inheritdoc />
    public DocumentAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }
}