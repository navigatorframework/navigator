using Navigator.Bundled.Actions.Messages;
using Navigator.Context.Accessor;
using Telegram.Bot.Types.Enums;

namespace Sample.Actions;

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