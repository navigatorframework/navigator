using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(EditedChannelPostAction))]
public abstract class EditedChannelPostAction : BaseAction
{
    /// <inheritdoc />
    protected EditedChannelPostAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

        

}