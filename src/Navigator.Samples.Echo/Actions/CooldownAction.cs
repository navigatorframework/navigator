using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram.Actions.Messages;

namespace Navigator.Samples.Echo.Actions;

[Cooldown]
public class CooldownAction : CommandAction
{
    public CooldownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return Command == "/cooldown";
    }
}