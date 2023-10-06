using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Providers.Telegram.Actions.Updates;

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