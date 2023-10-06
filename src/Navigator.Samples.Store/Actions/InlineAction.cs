using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Updates;

namespace Navigator.Samples.Store.Actions;

public class InlineAction : InlineQueryAction
{
    public InlineAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return true;
    }
}