using Navigator.Bundled.Actions.Messages;
using Navigator.Context.Accessor;

namespace Sample.Store.Actions;

public class EchoAction : TextAction
{
    public EchoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return !string.IsNullOrWhiteSpace(Text);
    }
}